using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseShipView : MonoBehaviour
{
    [SerializeField] private List<PalletView> _bagTargetPoints;
    [SerializeField] private float _rotationOffDistance = 10f;
    [SerializeField] private float _stopDistance = 0.2f;

    private Transform _targetPoint;
    private NavMeshAgent _agent;

    //public IReadOnlyList<PalletView> BagTargetPoints => _bagTargetPoints;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    //private void OnEnable()
    //{
    //    //_agent.SetDestination(_targetPoint.position);
    //}

    private void FixedUpdate()
    {
        if (_agent.hasPath)
        {
            if (_agent.remainingDistance <= _rotationOffDistance)
            {
                _agent.updateRotation = false;
                _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _targetPoint.rotation, _agent.angularSpeed/_rotationOffDistance * Time.deltaTime);
            }

            if (!_agent.isStopped && _agent.remainingDistance <= _stopDistance)
                _agent.isStopped = true;
        }
    }

    public void Initialize(Transform targetPoint)
    {
        //transform.position = spawnPoint;
        _targetPoint = targetPoint;

        _agent.SetDestination(_targetPoint.position);
    }

    public PalletView GetEmptyPallet()
    {
        return _bagTargetPoints.Find(pallet => pallet.HaveBag == false);
    }
}
