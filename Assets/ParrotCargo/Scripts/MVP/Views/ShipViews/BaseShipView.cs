using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using System.Linq;
using DG.Tweening;

public class BaseShipView : MonoBehaviour
{
    [SerializeField] private List<PalletView> _palletsForBags;
    [SerializeField] private CountPalletsFreeView _countPalletsFreeView;
    [SerializeField] private float _rotationOffDistance = 10f;
    [SerializeField] private float _stopDistance = 0.3f;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _isGoingToRelease;
    private bool _isGameOverSequenceStarted;
    private bool _isGameOverSinking;
    private ShipStopPoint _targetPoint;
    private NavMeshAgent _agent;

    public int EmptyPalletsCount => _palletsForBags.FindAll(pallet => pallet.HaveBag == false && pallet.gameObject.activeSelf).Count;
    public int FilledPalletsCount => _palletsForBags.FindAll(pallet => pallet.HaveBag).Count;
    public IReadOnlyList<PalletView> PalletViews => _palletsForBags;

    public ReactiveCommand Releasing;
    public ReactiveCommand ShipStopped = new ReactiveCommand();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    private void OnEnable()
    {
        transform.DOKill();
        _isGameOverSequenceStarted = false;
        _isGameOverSinking = false;
        _agent.enabled = true;
        Releasing = new();
    }

    private void OnDisable()
    {
        transform.DOKill();

        foreach (PalletView pallet in _palletsForBags)
            pallet.Clear();
    }

    private void FixedUpdate()
    {
        if (_agent.enabled == false)
            return;

        if (_agent.hasPath)
        {
            if (_agent.remainingDistance <= _rotationOffDistance && !_isGoingToRelease)
            {
                _agent.updateRotation = false;
                _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _targetPoint.transform.rotation, _agent.angularSpeed / _rotationOffDistance * Time.deltaTime);
            }

            if (!_agent.isStopped && _agent.remainingDistance <= _stopDistance)
            {
                _agent.isStopped = true;
                ShipStopped.Execute();
            }

            if (_agent.isStopped && _isGoingToRelease)
                Releasing.Execute();
        }
    }

    public virtual bool IsStopped()
    {
        if (_agent.enabled)
            return _agent.isStopped;
        else
            return false;
    }

    public void Initialize(ShipStopPoint targetPoint)
    {
        _palletsForBags.ForEach(pallet => pallet.gameObject.SetActive(false));

        _targetPoint = targetPoint;

        SetDestination(_targetPoint.transform.position, false);

        _palletsForBags.ForEach(pallet => pallet.EmptyChanged.Subscribe(haveBag =>
            { _countPalletsFreeView.UpdateCountPalletFree(EmptyPalletsCount); }));
    }

    public void SetDestination(Vector3 targetPosition, bool isGoingToRelease)
    {
        if (_isGameOverSequenceStarted)
            return;

        if (_agent.isOnNavMesh)
        {
            _agent.SetDestination(targetPosition);
            _agent.isStopped = false;
            _agent.updateRotation = true;
        }

        _isGoingToRelease = isGoingToRelease;

        if (_isGoingToRelease)
            _targetPoint.ChangeEmpty(true);
    }

    public void ActivatePallet()
    {
        if (_palletsForBags.Any(pallet => pallet.gameObject.activeSelf == false) == false)
            return;

        _palletsForBags.First(pallet => pallet.gameObject.activeSelf == false).gameObject.SetActive(true);

        _countPalletsFreeView.UpdateCountPalletFree(EmptyPalletsCount);
    }

    public List<BaseCrystallBagView> GetBagsOnShip()
    {
        List<BaseCrystallBagView> bags = new List<BaseCrystallBagView> ();

        foreach (var pallet in _palletsForBags)
        {
            var bag = pallet.GetBag();

            if (bag != null)
                bags.Add(bag);
        }

        return bags;
    }

    public void StartGameOverSinking(float targetY, float duration)
    {
        if (_isGameOverSinking)
            return;

        PrepareGameOverSequence();
        _isGameOverSinking = true;
        transform.DOKill();

        _particleSystem.Play();

        transform.DOMoveY(targetY, duration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true)
            .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    public void PrepareGameOverSequence()
    {
        if (_isGameOverSequenceStarted)
            return;

        _isGameOverSequenceStarted = true;
        _isGoingToRelease = false;

        if (_agent.enabled)
        {
            if (_agent.isOnNavMesh)
                _agent.ResetPath();

            _agent.isStopped = true;
            _agent.enabled = false;
        }
    }

    private void OnValidate()
    {
        if (_countPalletsFreeView == null)
            _countPalletsFreeView = gameObject.GetComponentInChildren<CountPalletsFreeView>();
    }
}

public class NullableShipView : BaseShipView
{
    public override bool IsStopped()
    {
        return false;
    }
}
