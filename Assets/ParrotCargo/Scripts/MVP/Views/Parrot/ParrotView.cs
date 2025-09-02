using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class ParrotView : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private Transform _bagPicker;
    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private float _bagOffset = 5f;

    private float _sittingWait = 2f;
    private Transform _parent;
    private Transform _targetPalletTransform;
    private Quaternion _startRotation;
    private Vector3 _startPosition;
    private Vector3 _continueMovingPosition;
    private BaseCrystallBagView _crystallBag;
    private BaseCrystallBagView _lastCrystallBag;
    private NavMeshAgent _agent;
    private Coroutine _sittingWithBagCoroutine;
    private WaitForSeconds _sittingWithBagWait;

    public BaseCrystallBagView CrystallBag => _crystallBag;

    public bool HaveBag { get; private set; }
    public bool IsTargetShip { get; private set; }
    public bool CanPick { get; private set; }

    public ReactiveCommand<bool> PickedBag = new ReactiveCommand<bool>();
    public ReactiveCommand<bool> DroppedBag = new ReactiveCommand<bool>();
    public ReactiveCommand ChangedActive = new ReactiveCommand();
    public ReactiveCommand<ParrotView> SittingWithBag = new ReactiveCommand<ParrotView>();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sittingWithBagWait = new WaitForSeconds(_sittingWait);
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _parent = transform.parent;
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        CanPick = false;
        HaveBag = false;
        transform.SetParent(_parent);
        ReturnToStartPoint();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf && _agent.enabled)
        {
            if (_agent.hasPath && _agent.remainingDistance < 1f && _agent.isStopped == false)
            {
                _agent.isStopped = true;

                if (IsTargetShip)
                    PutBag();
                else
                    SitWithBag();
            }
        }
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        ChangedActive.Execute();
    }

    public void SetParrotMovable(bool isMoving)
    {
        if (isMoving)
            _continueMovingPosition = transform.position;

        _agent.enabled = !isMoving;
    }

    public void ScanBag()
    {
        Ray ray = new Ray(_raycastPoint.position, Vector3.down);

        if (Physics.SphereCast(ray, 4f, out RaycastHit hit, 20f))
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
            _crystallBag.transform.SetParent(_bagPicker);
            _crystallBag.transform.position = _bagPicker.position;
            HaveBag = true;
        }

        PickedBag.Execute(HaveBag);
        _crystallBag.ChangePicked(HaveBag);
        _crystallBag.ReturnScale();
    }


    public void CarryBag(Transform targetPalletPosition, bool isTargetShip)
    {
        if (_sittingWithBagCoroutine != null)
        {
            StopCoroutine(_sittingWithBagCoroutine);
            _sittingWithBagCoroutine = null;
        }

        IsTargetShip = isTargetShip;
        _targetPalletTransform = targetPalletPosition;

        transform.SetParent(null);
        _agent.Warp(_continueMovingPosition);
        _agent.SetDestination(targetPalletPosition.position);
        _agent.baseOffset = targetPalletPosition.position.y + _bagOffset;
    }

    public void ReturnToStartPoint()
    {
        _agent.Warp(_startPosition);
        transform.localPosition = _startPosition;
        transform.localRotation = _startRotation;
    }

    private void PutBag()
    {
        Vector3 targetPosition = _targetPalletTransform.position;
        targetPosition.y += _bagOffset;

        _crystallBag.transform.SetParent(_targetPalletTransform);
        _crystallBag.transform.position = targetPosition;

        HaveBag = false;
        DroppedBag.Execute(HaveBag);
        SetActive(false);
    }

    private void SitWithBag()
    {
        Vector3 targetPosition = _targetPalletTransform.position;
        targetPosition.y += _bagOffset;

        transform.position = targetPosition;
        _continueMovingPosition = transform.position;

        if (_sittingWithBagCoroutine == null)
            _sittingWithBagCoroutine = StartCoroutine(CarryBagAfterSitOnPallet());
    }

    private IEnumerator CarryBagAfterSitOnPallet()
    {
        while (IsTargetShip == false)
        {
            SittingWithBag.Execute(this);

            yield return _sittingWithBagWait;
        }
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
}
