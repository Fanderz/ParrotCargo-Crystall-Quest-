using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class ParrotView : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private Transform _bagPicker;
    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private float _sitWithBagOffset = 6f;

    private bool _isMoving;
    //private bool _carryingBag;
    private bool _isTargetShip;
    private float _flyingOffset;

    public bool HaveBag { get; private set; }

    private Transform _parent;
    private Quaternion _startRotation;
    private Vector3 _startPosition;
    private Vector3 _continueMovingPosition;
    private BaseCrystallBagView _crystallBag;
    private BaseCrystallBagView _lastCrystallBag;
    private PalletView _targetPallet;
    private NavMeshAgent _agent;

    public ReactiveCommand<bool> PickedBag = new ReactiveCommand<bool>();

    public BaseCrystallBagView CrystallBag => _crystallBag;

    public ReactiveCommand Releasing = new ReactiveCommand();

    public bool CanPick { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _parent = transform.parent;
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        transform.SetParent(_parent);
        _agent.Warp(_startPosition);
        transform.localPosition = _startPosition;
        transform.localRotation = _startRotation;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _continueMovingPosition = transform.position;
            transform.position = new Vector3(_continueMovingPosition.x, _flyingOffset, _continueMovingPosition.z);
        }

        if (_agent.hasPath && _agent.remainingDistance < 1f && _agent.isStopped == false)
        {
            _agent.isStopped = true;

            if (_isTargetShip)
                PutBag();
            else
                SitWithBag();
        }
    }

    public void SetActive(bool value) =>
        gameObject.SetActive(value);

    public void SetMoving(bool value, float yOffset)
    {
        _isMoving = value;
        _agent.enabled = !value;
        _flyingOffset = yOffset;
    }

    public void SearchBag()
    {
        Ray ray = new Ray(_raycastPoint.position, Vector3.down);

        if (Physics.SphereCast(ray, 3f, out RaycastHit hit, 20f))
        {
            CanPick = hit.collider.TryGetComponent(out _crystallBag);

            if (CanPick)
                _crystallBag.RaiseOnRaycast();

            ReturnBagScale();

            _lastCrystallBag = _crystallBag;
        }
    }

    public void PickBag()
    {
        if (_crystallBag != null)
        {
            Vector3 startPosition = transform.position;

            _crystallBag.transform.SetParent(_bagPicker);
            _crystallBag.transform.position = _bagPicker.position;
            HaveBag = true;
            PickedBag.Execute(HaveBag);
            _crystallBag.ChangePicked(HaveBag);
            _crystallBag.ReturnScale();

            transform.position = startPosition;
        }
    }

    public void TryCarryBag(List<BaseShipView> ships, List<PalletView> pallets)
    {
        if (HaveBag)
        {
            _targetPallet = null;
            BaseShipView ship = ships.Find(ship => CheckBagExistsShip(ship));

            if (ship != null)
            {
                _targetPallet = ship.GetEmptyPallet();
                _isTargetShip = true;
            }
            else if(_targetPallet == null)
            {
                _targetPallet = pallets.Find(pallet => pallet.HaveBag == false);
                _isTargetShip = false;
            }

            if(_targetPallet != null)
                CarryBag(_targetPallet.BagTargetPosition);
        }
    }

    private void CarryBag(Vector3 target)
    {
        transform.SetParent(null);
        _agent.Warp(_continueMovingPosition);
        _agent.SetDestination(target);
        _agent.baseOffset = target.y;
        _targetPallet.TakeBag();
    }

    private void PutBag()
    {
        Vector3 targetPosition = new Vector3(_targetPallet.transform.position.x, _targetPallet.transform.position.y, _targetPallet.transform.position.z);
        //_targetPallet.TakeBag();
        _crystallBag.transform.SetParent(_targetPallet.transform);
        _crystallBag.transform.position = _targetPallet.BagTargetPosition;

        SetActive(false); // надо додумать
        Releasing.Execute();
    }

    private void SitWithBag()
    {
        Vector3 targetPosition = new Vector3(_targetPallet.BagTargetPosition.x, _targetPallet.BagTargetPosition.y + _sitWithBagOffset, _targetPallet.BagTargetPosition.z);
        transform.position = targetPosition;
        //_targetPallet.TakeBag();
    }

    private void ReturnBagScale()
    {
        if (_lastCrystallBag != null)
        {
            if (_crystallBag != null)
            {
                if (_lastCrystallBag.transform.position != _crystallBag.transform.position)
                    _lastCrystallBag.ReturnScale();
            }
            else
            {
                _lastCrystallBag.ReturnScale();
            }
        }
    }

    private bool CheckBagExistsShip(BaseShipView shipView)
    {
        if (shipView is BlueShipView && _crystallBag is BlueCrytallBagView)
            return true;
        else if (shipView is GoldShipView && _crystallBag is GoldCrystallBagView)
            return true;
        else if (shipView is GreenShipView && _crystallBag is GreenCrytallBagView)
            return true;
        else if (shipView is PurpleShipView && _crystallBag is PurpleCrystallBagView)
            return true;
        else
            return false;
    }
}
