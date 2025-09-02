using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;

public class BaseShipView : MonoBehaviour
{
    [SerializeField] private List<PalletView> _palletsForBags;
    [SerializeField] private float _rotationOffDistance = 10f;
    [SerializeField] private float _stopDistance = 0.2f;

    private bool _isGoingToRelease;
    private ShipStopPoint _targetPoint;
    private NavMeshAgent _agent;

    public int EmptyPalletsCount => _palletsForBags.FindAll(pallet => pallet.HaveBag == false).Count;
    public IReadOnlyList<PalletView> PalletViews => _palletsForBags;

    public ReactiveCommand Releasing = new ReactiveCommand();

    public bool IsStopped()
    {
        if (_agent.enabled)
            return _agent.isStopped;
        else
            return false;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    private void OnEnable()
    {
        _agent.enabled = true;

        foreach (PalletView pallet in _palletsForBags)
        {
            pallet.RemoveBag();

            BaseCrystallBagView bag = pallet.GetBag();

            if (bag != null)
                bag.Release();
        }
    }

    private void OnDisable()
    {
        _agent.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_agent.hasPath)
        {
            if (_agent.remainingDistance <= _rotationOffDistance && !_isGoingToRelease)
            {
                _agent.updateRotation = false;
                _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _targetPoint.transform.rotation, _agent.angularSpeed/_rotationOffDistance * Time.deltaTime);
            }

            if (!_agent.isStopped && _agent.remainingDistance <= _stopDistance)
                _agent.isStopped = true;

            if (_agent.isStopped && _isGoingToRelease)
                Releasing.Execute();
        }
    }

    public void Initialize(ShipStopPoint targetPoint)
    {
        _targetPoint = targetPoint;

        SetDestination(_targetPoint.transform.position, false);
    }

    public PalletView GetEmptyPallet()
    {
        return _palletsForBags.Find(pallet => pallet.HaveBag == false);
    }

    public void OccupyPallet(PalletView pallet, BaseCrystallBagView crystallBagView)
    {
        if (_palletsForBags.Contains(pallet))
            pallet.TakeBag(crystallBagView);
    }

    public void SetDestination(Vector3 targetPosition, bool isGoingToRelease)
    {
        _agent.SetDestination(targetPosition);
        _agent.isStopped = false;
        _agent.updateRotation = true;
        _isGoingToRelease = isGoingToRelease;


        if (_isGoingToRelease)
            _targetPoint.ChangeEmpty(true);
    }
}
