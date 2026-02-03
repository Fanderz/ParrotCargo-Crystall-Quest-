using System.Collections.Generic;

using UnityEngine;

using UniRx;

public class ParrotsBlockView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _zOffsetOnPick;
    [SerializeField] private List<ParrotView> _parrots;
    [SerializeField] private TypeParrotsBlock _typeParrotsBlock;

    private Vector3 _startPosition;
    private float _zPickingValue;
    private Coroutine _sittingWithBagCoroutine;
    private WaitForSeconds _sittingWithBagWait;
    private DraggableParrotBlock _draggableBlock;

    public bool EachParrotHaveBag => _parrots.FindAll(parrot => parrot.isActiveAndEnabled).TrueForAll(parrot => parrot.HaveBag == true);
    public IReadOnlyList<ParrotView> Parrots => _parrots;

    public bool IsMoving { get; private set; }
    public bool CanPickBag { get; private set; }
    public TypeParrotsBlock TypeParrotsBlock => _typeParrotsBlock;

    public ReactiveCommand<Vector3> BlockMoving = new ReactiveCommand<Vector3>();
    public ReactiveCommand<bool> Movable = new ReactiveCommand<bool>();
    public ReactiveCommand<bool> Activation = new ReactiveCommand<bool>();
    public ReactiveCommand<ParrotView> SearchingRecievers = new ReactiveCommand<ParrotView>();

    private void Awake()
    {
        _sittingWithBagWait = new WaitForSeconds(2f);

        _parrots.ForEach(parrotView => parrotView.SittingWithBag.Subscribe(view =>
        {
            SearchingRecievers.Execute(view);
        }));
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        IsMoving = false;
        CanPickBag = false;

        if (_sittingWithBagCoroutine != null)
            StopCoroutine(_sittingWithBagCoroutine);
    }

    public void Initialize(GameObject prefabBird)
    {
        _startPosition = transform.position;
        _zPickingValue = transform.position.z - _zOffsetOnPick / 2;

        ActivateChildParrots();
        _draggableBlock = GetComponent<DraggableParrotBlock>();
        GetComponent<BoxCollider>().enabled = true;

        foreach (var parrotView in _parrots)
        {
            if (parrotView.ViewSkin == null)
            {
                var bird = Instantiate(prefabBird, parrotView.transform);
                parrotView.SetSkin(bird);

                var birdAnimator = bird.GetComponent<Animator>();

                if (birdAnimator != null)
                    parrotView.SetChildAnimator(birdAnimator);
            }
        }
    }

    public void MoveBlock(Vector3 newPosition)
    {
        IsMoving = true;
        transform.position = newPosition;

        BlockMoving.Execute(transform.position);
        Movable.Execute(IsMoving);
    }

    public void StopMoveBlock()
    {
        IsMoving = false;

        BlockMoving.Execute(transform.position);
        Movable.Execute(IsMoving);
    }

    public void ScanBags()
    {
        foreach (ParrotView parrot in _parrots)
        {
            if (parrot.gameObject.activeSelf)
                parrot.ScanBag();
        }

        CanPickBag = _parrots.TrueForAll(parrot => parrot.CanPick == true);
    }

    public void PickBags()
    {
        MoveBlock(new Vector3(transform.position.x, transform.position.y, _zPickingValue));
        StopMoveBlock();

        if (CanPickBag)
        {
            foreach (ParrotView parrot in _parrots)
                parrot.PickBag();
        }
    }

    public void ReturnToBase()
    {
        MoveBlock(_startPosition);
        StopMoveBlock();
    }

    public void Release()
    {
        BlockMoving = new();
        Movable = new();
        Activation = new();
        SearchingRecievers = new();
    }

    private void ActivateChildParrots()
    {
        foreach (ParrotView parrotView in _parrots)
            parrotView.SetActive(true);
    }
}
