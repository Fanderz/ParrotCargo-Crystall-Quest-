using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseShipView : MonoBehaviour
{
    [SerializeField] private List<Transform> _bagTargetPoints;
    [SerializeField] private float _rotationOffDistance = 10f;
    [SerializeField] private float _stopDistance = 0.2f;

    private Transform _targetPoint;
    private NavMeshAgent _agent;

    public IReadOnlyList<Transform> BagTargetPoints => _bagTargetPoints;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

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

    public void Initialize(Transform targetPoint, Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        _targetPoint = targetPoint;

        _agent.SetDestination(_targetPoint.position);
    }
}
