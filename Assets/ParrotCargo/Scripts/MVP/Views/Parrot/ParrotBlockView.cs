using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System.Collections;

public class ParrotsBlockView : MonoBehaviour
{
    [SerializeField] private float _zOffsetOnPick;
    [SerializeField] private float _scanningBagsDelay;
    [SerializeField] private List<ParrotView> _parrots;

    private float _zPickingValue;
    private Vector3 _startPosition;
    private bool _isMoving;
    private bool _canPick;

    private DraggableParrotBlock _draggable;
    private Coroutine _searchingCoroutine;
    private WaitForSeconds _scanningDelay;

    public IReadOnlyList<ParrotView> Parrots => _parrots;

    public ReactiveCommand<Vector3> BlockMoving = new ReactiveCommand<Vector3>();
    public ReactiveCommand<bool> Movable = new ReactiveCommand<bool>();
    //public ReactiveCommand<bool> AllCanPick = new ReactiveCommand<bool>();


    private void Awake()
    {
        _draggable = GetComponent<DraggableParrotBlock>();
        _scanningDelay = new WaitForSeconds(_scanningBagsDelay);
    }

    public void Initialize()
    {
        //ActivateRandomParrots();

        _startPosition = transform.position;
        _zPickingValue = transform.position.z - _zOffsetOnPick / 2;

        Subscribes();
    }

    public void TryPickBags()
    {
        StopMovingBlock();

        if (_canPick)
        {
            PickBags();
        }
        else
        {
            ReturnToBase();
        }
    }

    private void Subscribes()
    {
        _draggable.MoveCommand.Subscribe(targetPosition =>
        {
            _isMoving = true;
            MoveBlock(new Vector3(targetPosition.x, _startPosition.y + _draggable.YFlyingOffset, targetPosition.z));
            ScannBags();
            //_searchingCoroutine = StartCoroutine(ScanBags());
        });

        _draggable.StopMoving.Subscribe(pickBag => 
        {
            _isMoving = false;
            TryPickBags(); 
        });
    }

    private void PickBags()
    {
        MoveBlock(new Vector3(transform.position.x, transform.position.y, _zPickingValue));

        foreach (ParrotView parrot in _parrots)
            parrot.PickBag();
    }

    private void ReturnToBase()
    {
        MoveBlock(_startPosition);
    }

    private void ActivateRandomParrots()
    {
        var activeParrotsCount = Random.Range(1, _parrots.Count - 1);

        for (int i = 0; i < activeParrotsCount; i++)
        {
            _parrots[Random.Range(0, _parrots.Count)].SetActive(true);
        }
    }

    private void MoveBlock(Vector3 newPosition)
    {
        transform.position = newPosition;

        BlockMoving.Execute(transform.position);
        Movable.Execute(_isMoving);
    }

    private void StopMovingBlock()
    {
        Movable.Execute(_isMoving);

        //if (_searchingCoroutine != null)
        //    StopCoroutine(_searchingCoroutine);
    }


    private IEnumerator ScanBags()
    {
        while (_isMoving)
        {
            foreach (ParrotView parrot in _parrots)
            {
                if (parrot.gameObject.activeSelf)
                    parrot.SearchBag();
            }

            _canPick = _parrots.TrueForAll(canPick => canPick.CanPick == true);
            //AllCanPick.Execute(_canPick);

            yield return _scanningDelay;
        }
    }

    private void ScannBags()
    {
        if (_isMoving)
        {
            foreach (ParrotView parrot in _parrots)
            {
                if (parrot.gameObject.activeSelf)
                    parrot.SearchBag();
            }

            _canPick = _parrots.TrueForAll(canPick => canPick.CanPick == true);
            //AllCanPick.Execute(_canPick);
        }
    }
}
