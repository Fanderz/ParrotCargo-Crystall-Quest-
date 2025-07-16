using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class ParrotsBlockView : MonoBehaviour
{
    [SerializeField] private float _zOffsetOnPick;
    [SerializeField] private List<ParrotView> _parrots;

    private float _zPickingValue;
    private bool _isMoving;
    private bool _canPick;


    private List<BaseShipView> _shipViews;
    private List<PalletView> _pallets;

    private DraggableParrotBlock _draggable;

    public IReadOnlyList<ParrotView> Parrots => _parrots;

    public Vector3 StartPosition { get; private set; }

    public ReactiveCommand<Vector3> BlockMoving = new ReactiveCommand<Vector3>();
    public ReactiveCommand<bool> Movable = new ReactiveCommand<bool>();
    public ReactiveCommand ReleasingBlock = new ReactiveCommand();
    public ReactiveCommand SearchingRecievers = new ReactiveCommand();

    private void Awake()
    {
        _draggable = GetComponent<DraggableParrotBlock>();
    }

    private void OnEnable()
    {
        foreach (ParrotView parrot in _parrots)
            parrot.SetActive(true);
    }

    public void Initialize()
    {
        StartPosition = transform.position;
        _zPickingValue = transform.position.z - _zOffsetOnPick / 2;

        Subscribes();
    }

    public void SetReceivers(List<BaseShipView> ships, List<PalletView> pallets)
    {
        _shipViews = ships;
        _pallets = pallets;
    }

    private void Subscribes()
    {
        foreach (ParrotView parrot in _parrots)
        {
            parrot.Releasing.Subscribe(release => { TryReleaseBlock(); });
        }

        //_draggable.MoveCommand.Subscribe(targetPosition =>
        //{
        //    _isMoving = true;
        //    Vector3 newPosition = new Vector3(targetPosition.x, StartPosition.y + _draggable.YFlyingOffset, targetPosition.z);

        //    MoveBlock(newPosition);
        //    ScanBags();
        //});

        _draggable.StopMoving.Subscribe(pickBag =>
        {
            _isMoving = false;
            TryPickBags();
            CarryBags();
        });
    }

    private void TryReleaseBlock()
    {
        if (_parrots.TrueForAll(parrot => parrot.gameObject.activeSelf == false))
            ReleasingBlock.Execute();
    }

    //private void MoveBlock(Vector3 newPosition)
    //{
    //    transform.position = newPosition;

    //    SetParrotsMovable(newPosition.y);

    //    BlockMoving.Execute(transform.position);
    //    Movable.Execute(_isMoving);
    //}
    public void MoveBlock(Vector3 newPosition)
    {
        _isMoving = true;
        transform.position = newPosition;

        SetParrotsMovable(newPosition.y);

        BlockMoving.Execute(transform.position);
        Movable.Execute(_isMoving);
    }

    public void StopMoveBlock()
    {
        _isMoving = false;

        BlockMoving.Execute(transform.position);
        Movable.Execute(_isMoving);
    }

    private void SetParrotsMovable(float yOffset)
    {
        foreach (ParrotView parrot in _parrots)
            parrot.SetMoving(_isMoving, yOffset);
    }

    //private void ScanBags()
    //{
    //    if (_isMoving)
    //    {
    //        foreach (ParrotView parrot in _parrots)
    //        {
    //            if (parrot.gameObject.activeSelf)
    //                parrot.SearchBag();
    //        }

    //        _canPick = _parrots.TrueForAll(parrot => parrot.CanPick == true);
    //    }
    //}
    public void ScanBags()
    {
        if (_isMoving)
        {
            foreach (ParrotView parrot in _parrots)
            {
                if (parrot.gameObject.activeSelf)
                    parrot.SearchBag();
            }

            _canPick = _parrots.TrueForAll(parrot => parrot.CanPick == true);
        }
    }



    //private void TryPickBags()
    //{
    //    StopMovingBlock();

    //    if (_canPick)
    //        PickBags();
    //    else if (_parrots.TrueForAll(parrot => parrot.HaveBag == false))
    //        ReturnToBase();
    //}
    public void TryPickBags()
    {
        StopMovingBlock();

        if (_canPick)
            PickBags();
        else if (_parrots.TrueForAll(parrot => parrot.HaveBag == false))
            ReturnToBase();
    }

    //private void StopMovingBlock()
    //{
    //    Movable.Execute(_isMoving);
    //    SetParrotsMovable(transform.position.y);
    //}
    public void StopMovingBlock()
    {
        Movable.Execute(_isMoving);
        SetParrotsMovable(transform.position.y);
    }

    private void PickBags()
    {
        MoveBlock(new Vector3(transform.position.x, transform.position.y, _zPickingValue));

        foreach (ParrotView parrot in _parrots)
            parrot.PickBag();
    }

    private void ReturnToBase()
    {
        MoveBlock(StartPosition);
    }

    //private void CarryBags()
    //{
    //    if (_parrots.TrueForAll(parrot => parrot.HaveBag == true))
    //    {
    //        _draggable.enabled = false;
    //        SearchingRecievers.Execute();

    //        foreach (ParrotView parrot in _parrots)
    //        {
    //            parrot.TryCarryBag(_shipViews, _pallets);
    //        }
    //    }
    //}
    public void CarryBags()
    {
        if (_parrots.TrueForAll(parrot => parrot.HaveBag == true))
        {
            _draggable.enabled = false;
            SearchingRecievers.Execute();

            foreach (ParrotView parrot in _parrots)
            {
                parrot.TryCarryBag(_shipViews, _pallets);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    float step = 1f;
    //    float radius = 100f;
    //    Vector3 direction = transform.forward;
    //    Vector3 origin = transform.position;

    //    for (float i = 0; i < 30f; i += step)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(origin + direction * i, radius);
    //    }
    //}
}
