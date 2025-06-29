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

    private bool _movingToShip;
    private bool _moving;
    private float _flyingOffset;

    public bool HaveBag { get; private set; }

    private BaseCrystallBagView _crystallBag;
    private BaseCrystallBagView _lastCrystallBag;
    private NavMeshAgent _agent;

    public ReactiveCommand<bool> PickedBag = new ReactiveCommand<bool>();

    public BaseCrystallBagView CrystallBag => _crystallBag;

    public bool CanPick { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    private void Update()
    {
        if (_moving)
        {
            Vector3 startPosition = transform.position;
            transform.position = new Vector3(startPosition.x, _flyingOffset, startPosition.z);
        }
    }

    public void SetActive(bool value) =>
        gameObject.SetActive(value);

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

            //PickingBag.Execute(CanPick);
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

    public void TryCarryBag(Vector3 target)
    {
        if (HaveBag)
        {
            CarryBag(target);
        }
    }

    private void CarryBag(Vector3 target)
    {
        _agent.SetDestination(target);
        _agent.baseOffset = target.y;
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

    public void SetMoving(bool value, float yOffset)
    {
        _moving = value;
        _flyingOffset = yOffset;
    }
}
