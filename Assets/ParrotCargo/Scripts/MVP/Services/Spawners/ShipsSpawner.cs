using System.Collections.Generic;

using UnityEngine;

using Zenject;
using UniRx;
using YG;

public class ShipsSpawner : BaseSpawner<BaseShipView>
{
    [SerializeField] private List<ShipStopPoint> _targetPoints;
    [SerializeField] private Transform _pointToRelease;

    private List<ShipPresenter> _shipPresenters;
    private DiContainer _container;

    public IReadOnlyList<ShipPresenter> ShipPresenters => _shipPresenters;

    public void Initialize()
    {
        _shipPresenters = new List<ShipPresenter>();
    }

    public void Spawn()
    {
        var emptyPoints = _targetPoints.FindAll(point => point.isEmpty);
        var activePallets = YG2.saves.shopModel.ShipPalletsCnt;

        for (int i = 0; i < emptyPoints.Count; i++)
        {
            var ship = new Ship(_pointToRelease.position, activePallets);

            var shipView = SpawnObject(SpawnPoints[i].position);

            var shipPresenter = new ShipPresenter(shipView, ship);
            shipPresenter.Initialize(activePallets, emptyPoints[i]);
            shipPresenter.Releasing.Subscribe(presenter => { Spawn(); });
            _shipPresenters.Add(shipPresenter);

            shipView.Releasing.Subscribe(view => 
            { 
                Release(shipView); 
                _shipPresenters.Remove(_shipPresenters.Find(ship => ship.GetView() == shipView));
            });

            emptyPoints[i].ChangeEmpty(false);
        }
    }

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
