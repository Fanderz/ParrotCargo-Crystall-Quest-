using System.Linq;

using UnityEngine;
using UnityEngine.AI;

using UniRx;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using System.Threading;
using System;
using DG.Tweening;

public class ParrotView : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private Transform _bagPicker;
    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private float _bagOffset = 5f;
    [SerializeField] private Animator _childAnimator;

    private float _rotationTime = 0.3f;
    private int _waitMiliseconds = 1000;

    private Transform _parent;
    private Transform _targetPalletTransform;
    private Quaternion _agentStartRotation;
    private Quaternion _startRotation;
    private Vector3 _startPosition;
    private Vector3 _continueMovingPosition;
    private BaseCrystallBagView _crystallBag;
    private BaseCrystallBagView _lastCrystallBag;
    private NavMeshAgent _agent;
    private CancellationTokenSource _cancellationToken;

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
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _parent = transform.parent;
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
        _agentStartRotation = _agent.transform.rotation;
    }

    private void OnEnable()
    {
        CanPick = false;
        HaveBag = false;
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf && _agent.enabled)
        {
            if (_agent.hasPath && _agent.remainingDistance < 1.5f && _agent.isStopped == false)
            {
                _agent.isStopped = true;

                if (IsTargetShip == false)
                    SitWithBag();
            }
        }
    }

    private void OnDestroy()
    {
        _cancellationToken?.Cancel();
    }

    public void SetChildAnimator(Animator animator)
    {
        var s = transform.GetChild(0);
        s.gameObject.SetActive(false);
        _childAnimator = animator;
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
            _crystallBag?.RaiseOnRaycast();
        else
            _crystallBag?.ReturnScale();

        ReturnBagScale();

        if (_crystallBag != null)
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
        _crystallBag?.ReturnScale();
    }

    public async void CarryBag(Transform targetPalletPosition, bool isTargetShip)
    {
        _agent.updateRotation = true;
        _childAnimator.SetTrigger("Flying");
        IsTargetShip = isTargetShip;
        _targetPalletTransform = targetPalletPosition;

        transform.SetParent(null);
        _agent.Warp(_continueMovingPosition);
        _agent.SetDestination(targetPalletPosition.position);
        _agent.baseOffset = targetPalletPosition.position.y + _bagOffset;

        while (_agent.IsDestroyed() == false && _agent?.hasPath == false)
            await UniTask.Delay(_waitMiliseconds);

        if (isTargetShip)
            await PutBag();
    }

    public void ReturnToStartPoint()
    {
        _agent.Warp(_startPosition);
        _agent.baseOffset = 0f;
        transform.localPosition = _startPosition;
        transform.localRotation = _startRotation;
    }

    private async UniTask PutBag()
    {
        if (_agent == null)
            return;

        _cancellationToken = new CancellationTokenSource();

        try
        {
            await UniTask.WaitUntil(() => _agent.remainingDistance <= 0.4f, cancellationToken: _cancellationToken.Token);
        }
        catch(OperationCanceledException)
        {
            return;
        }

        _crystallBag.transform.SetParent(_targetPalletTransform.transform);

        HaveBag = false;
        DroppedBag.Execute(HaveBag);
        _agent.isStopped = true;
        SetActive(false);
        Reset();
    }

    private async void SitWithBag()
    {
        _childAnimator.SetTrigger("Sitting");
        Vector3 targetPosition = _targetPalletTransform.position;
        targetPosition.y += _bagOffset;

        transform.position = targetPosition;
        _agent.updateRotation = false;
        _agent.transform.DORotateQuaternion(_agentStartRotation, _rotationTime).SetEase(Ease.Linear);
        //_agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _agentStartRotation, _rotationSlerpTime * Time.deltaTime);

        _continueMovingPosition = transform.position;

        while (IsTargetShip == false)
        {
            SittingWithBag.Execute(this);
            await UniTask.Delay(_waitMiliseconds);
        }
    }

    private void ReturnBagScale()
    {
        if (_lastCrystallBag?.transform.position != _crystallBag?.transform.position)
            _lastCrystallBag?.ReturnScale();
    }

    private void Reset()
    {
        transform.SetParent(_parent);
        ReturnToStartPoint();
        PickedBag = new();
        DroppedBag = new();
    }

    private bool IsHittedBag(RaycastHit[] hits)
    {
        return hits.Any(hit => hit.collider.TryGetComponent(out _crystallBag));
    }
}
