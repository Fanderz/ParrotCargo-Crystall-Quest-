using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShipsSpawner : BaseSpawner<BaseShipView>
{
    [SerializeField] private List<Transform> _targetPoints;

    private List<ShipPresenter> _shipPresenters;
    private DiContainer _container;

    public IReadOnlyList<ShipPresenter> ShipPresenters => _shipPresenters;

    public void Initialize()
    {
        _shipPresenters = new List<ShipPresenter>();
    }

    public void Spawn()
    {
        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            var ship = new Ship();
            var shipView = SpawnObject(SpawnPoints[i].position);
            shipView.Initialize(_targetPoints[i]);
            var shipPresenter = new ShipPresenter(shipView, ship);

            _shipPresenters.Add(shipPresenter);
        }
    }

    //public ShipPresenter GetShip(int index)
    //{
    //    return _shipPresenters[index];
    //}

    protected override void CreatePool()
    {
        if (Pool == null)
            Pool = new BasePool<BaseShipView>(ObjectsMaxCount, Parent, _container);
    }

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        CreatePool();
    }
}
