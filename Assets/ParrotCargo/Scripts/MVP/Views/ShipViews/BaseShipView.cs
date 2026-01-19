using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using System.Linq;

public class BaseShipView : MonoBehaviour
{
    [SerializeField] private List<PalletView> _palletsForBags;
    [SerializeField] private CountPalletsFreeView _countPalletsFreeView;
    [SerializeField] private float _rotationOffDistance = 10f;
    [SerializeField] private float _stopDistance = 0.3f;

    private bool _isGoingToRelease;
    private ShipStopPoint _targetPoint;
    private NavMeshAgent _agent;

    public int EmptyPalletsCount => _palletsForBags.FindAll(pallet => pallet.HaveBag == false && pallet.gameObject.activeSelf).Count;
    public IReadOnlyList<PalletView> PalletViews => _palletsForBags;

    public ReactiveCommand Releasing;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    private void OnEnable()
    {
        _agent.enabled = true;
        Releasing = new();
    }

    private void OnDisable()
    {
        foreach (PalletView pallet in _palletsForBags)
            pallet.Clear();
    }

    private void FixedUpdate()
    {
        if (_agent.hasPath)
        {
            if (_agent.remainingDistance <= _rotationOffDistance && !_isGoingToRelease)
            {
                _agent.updateRotation = false;
                _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _targetPoint.transform.rotation, _agent.angularSpeed / _rotationOffDistance * Time.deltaTime);
            }

            if (!_agent.isStopped && _agent.remainingDistance <= _stopDistance)
                _agent.isStopped = true;

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
        _targetPoint = targetPoint;

        SetDestination(_targetPoint.transform.position, false);

        foreach (var palletView in _palletsForBags)
            palletView.EmptyChanged.Subscribe(haveBag => { _countPalletsFreeView.UpdateCountPalletFree(EmptyPalletsCount); });
    }

    public void SetDestination(Vector3 targetPosition, bool isGoingToRelease)
    {
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
        if(_palletsForBags.Any(pallet => pallet.gameObject.activeSelf == false) == false)
                return;

        _palletsForBags.First(pallet => pallet.gameObject.activeSelf == false).gameObject.SetActive(true);

        _countPalletsFreeView.UpdateCountPalletFree(EmptyPalletsCount);
    }

    private void OnValidate()
    {
        if(_countPalletsFreeView == null)
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
