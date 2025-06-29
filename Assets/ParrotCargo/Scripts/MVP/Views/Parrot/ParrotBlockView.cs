using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System.Collections;

public class ParrotsBlockView : MonoBehaviour
{
    [SerializeField] private float _zOffsetOnPick;
    [SerializeField] private float _scanningBagsDelay;
    [SerializeField] private List<ParrotView> _parrots;
    [SerializeField] private LayerMask _shipsLayer;

    private float _zPickingValue;
    private Vector3 _startPosition;
    private bool _isMoving;
    private bool _canPick;
    private List<PalletView> _findedPallets;

    private DraggableParrotBlock _draggable;

    public IReadOnlyList<ParrotView> Parrots => _parrots;

    public ReactiveCommand<Vector3> BlockMoving = new ReactiveCommand<Vector3>();
    public ReactiveCommand<bool> Movable = new ReactiveCommand<bool>();


    private void Awake()
    {
        _draggable = GetComponent<DraggableParrotBlock>();
    }

    public void Initialize()
    {
        _startPosition = transform.position;
        _zPickingValue = transform.position.z - _zOffsetOnPick / 2;

        Subscribes();
    }


    private void Subscribes()
    {
        _draggable.MoveCommand.Subscribe(targetPosition =>
        {
            _isMoving = true;
            MoveBlock(new Vector3(targetPosition.x, _startPosition.y + _draggable.YFlyingOffset, targetPosition.z));
            ScannBags();
        });

        _draggable.StopMoving.Subscribe(pickBag =>
        {
            _isMoving = false;
            TryPickBags();
            CarryBags();
        });
    }

    private void MoveBlock(Vector3 newPosition)
    {
        transform.position = newPosition;

        SetParrotsMovable(newPosition.y);

        BlockMoving.Execute(transform.position);
        Movable.Execute(_isMoving);
    }

    private void SetParrotsMovable(float yOffset)
    {
        foreach (ParrotView parrot in _parrots)
        {
            parrot.SetMoving(_isMoving, yOffset);
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

            _canPick = _parrots.TrueForAll(parrot => parrot.CanPick == true);
        }
    }

    private void TryPickBags()
    {
        StopMovingBlock();

        if (_canPick)
            PickBags();
        else if (_parrots.TrueForAll(parrot => parrot.HaveBag == false))
            ReturnToBase();
    }

    private void StopMovingBlock()
    {
        Movable.Execute(_isMoving);
        SetParrotsMovable(transform.position.y);
    }

    private void PickBags()
    {
        MoveBlock(new Vector3(transform.position.x, transform.position.y, _zPickingValue));

        foreach (ParrotView parrot in _parrots)
            parrot.PickBag();

        //_startPosition = transform.position;
        //transform.position = _startPosition;
    }

    private void ReturnToBase()
    {
        MoveBlock(_startPosition);
    }

    private void CarryBags()
    {
        int i = 0;
        int j = 0;

        if (_parrots.TrueForAll(parrot => parrot.HaveBag == true))
        {
            foreach (ParrotView parrot in _parrots)
            {
                if (parrot.HaveBag)
                {
                    IReadOnlyList<BaseShipView> ships = SearchShips();

                    foreach (BaseShipView shipView in ships)
                    {
                        if (CheckBagExistsShip(shipView, parrot.CrystallBag))
                        {
                            parrot.TryCarryBag(shipView.BagTargetPoints[i].position);
                            i++;
                        }
                        else
                        {
                            if (_findedPallets.Count > 0)
                            {
                                if (_findedPallets[j].IsEmpty)
                                {
                                    parrot.TryCarryBag(_findedPallets[j].transform.position);
                                    _findedPallets[j].ChangeEmpty(false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private IReadOnlyList<BaseShipView> SearchShips()
    {
        List<BaseShipView> ships = new List<BaseShipView>();
        _findedPallets = new List<PalletView>();

        RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, 120f, Vector3.forward, 200f, _shipsLayer);

        if (hitInfo.Length > 0)
        {
            for (int i = 0; i < hitInfo.Length; i++)
            {
                if (hitInfo[i].collider.TryGetComponent(out BaseShipView ship))
                    ships.Add(ship);
                else if (hitInfo[i].collider.TryGetComponent(out PalletView pallet))
                    _findedPallets.Add(pallet);
            }
        }

        return ships;
    }

    private bool CheckBagExistsShip(BaseShipView shipView, BaseCrystallBagView crystallBagView)
    {
        if (shipView is BlueShipView && crystallBagView is BlueCrytallBagView)
            return true;
        else if (shipView is GoldShipView && crystallBagView is GoldCrystallBagView)
            return true;
        else if (shipView is GreenShipView && crystallBagView is GreenCrytallBagView)
            return true;
        else if (shipView is PurpleShipView && crystallBagView is PurpleCrystallBagView)
            return true;
        else
            return false;
    }

    //private void OnDrawGizmos()
    //{
    //    float step = 1f;
    //    float radius = 70f;
    //    Vector3 direction = transform.forward;
    //    Vector3 origin = transform.position;

    //    for (float i = 0; i < 200f; i += step)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(origin + direction * i, radius);
    //    }
    //}
}
