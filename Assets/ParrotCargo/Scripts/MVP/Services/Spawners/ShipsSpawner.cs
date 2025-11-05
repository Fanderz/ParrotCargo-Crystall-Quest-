using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System.Linq;

public class ShipsSpawner : BaseSpawner<BaseShipView>
{
    [SerializeField] private List<ShipStopPoint> _targetPoints;
    [SerializeField] private Transform _pointToRelease;
    //[SerializeField] private int _activeObjectsCnt;

    private List<ShipPresenter> _shipPresenters;
    private DiContainer _container;

    public IReadOnlyList<ShipPresenter> ShipPresenters => _shipPresenters;

    public void Initialize(List<BaseShipView> shipViews)
    {
        _shipPresenters = new List<ShipPresenter>();
        Prefab = shipViews;
    }

    public void Spawn()
    {
        var emptyPoints = _targetPoints.FindAll(point => point.isEmpty);

        for (int i = 0; i < emptyPoints.Count; i++)
        {
            var ship = new Ship(_pointToRelease.position);

            var shipView = SpawnObject(SpawnPoints[i].position);
            shipView.Initialize(emptyPoints[i]);


            shipView.Releasing.Subscribe(view => 
            { 
                Release(shipView); 
                _shipPresenters.Remove(_shipPresenters.Find(ship => ship.GetView() == shipView));
                //shipView.PalletViews.ToList().ForEach(pallet => pallet.RemoveBag());
            });

            var shipPresenter = new ShipPresenter(shipView, ship);
            shipPresenter.Initialize();
            shipPresenter.Releasing.Subscribe(presenter => { Spawn(); });

            _shipPresenters.Add(shipPresenter);

            emptyPoints[i].ChangeEmpty(false);
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
