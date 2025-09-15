using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using System;

using Cysharp.Threading.Tasks;

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
    private Coroutine _waitForArriveToPutBagCoroutine;
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
        //transform.SetParent(_parent);
        //ReturnToStartPoint();
    }

    private void OnDisable()
    {

    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf && _agent.enabled)
        {
            if (_agent.hasPath && _agent.remainingDistance < 1f && _agent.isStopped == false)
            {
                _agent.isStopped = true;

                if (IsTargetShip == false)
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
        RaycastHit[] hits = Physics.SphereCastAll(ray, 2f, 20f, 1 << LayerMask.NameToLayer("PickableLayer"));

        CanPick = IsHittedBag(hits);

        if (CanPick)
            _crystallBag.RaiseOnRaycast();

        ReturnBagScale();

        _lastCrystallBag = _crystallBag;
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

    public async void CarryBag(Transform targetPalletPosition, bool isTargetShip)
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

        while (_agent.hasPath == false)
            await UniTask.Delay(1000);

        if (isTargetShip)
            _waitForArriveToPutBagCoroutine = StartCoroutine(PutBagOnArrive());
    }

    public void ReturnToStartPoint()
    {
        _agent.Warp(_startPosition);
        _agent.baseOffset = 0f;
        transform.localPosition = _startPosition;
        transform.localRotation = _startRotation;
    }

    private void PutBag()
    {
        if (_waitForArriveToPutBagCoroutine != null)
        {
            StopCoroutine(_waitForArriveToPutBagCoroutine);
            _waitForArriveToPutBagCoroutine = null;
        }

        _crystallBag.transform.SetParent(_targetPalletTransform.transform);

        HaveBag = false;
        DroppedBag.Execute(HaveBag);
        SetActive(false);
        Reset();
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

    private IEnumerator PutBagOnArrive()
    {
        while (_agent.remainingDistance > 0.05f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("PutBagOnArrive RemainingDistance: " + _agent.remainingDistance);
        PutBag();
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
                Debug.Log("ReturnScale");
                _lastCrystallBag.ReturnScale();
            }
        }
    }

    private void Reset()
    {
        transform.SetParent(_parent);
        ReturnToStartPoint();
        PickedBag = new();
        DroppedBag = new();
        //SittingWithBag = new();

        if (_sittingWithBagCoroutine != null || _waitForArriveToPutBagCoroutine != null)
        {
            StopAllCoroutines();

            _sittingWithBagCoroutine = null;
            _waitForArriveToPutBagCoroutine = null;
        }
    }

    private bool IsHittedBag(RaycastHit[] hits)
    {
        return hits.Any(hit => hit.collider.TryGetComponent(out _crystallBag));
    }
}
