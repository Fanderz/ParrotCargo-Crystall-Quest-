using System.Collections.Generic;
using System.Linq;

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

    public void Initialize(List<BaseShipView> prefabs)
    {
        _shipPresenters = new List<ShipPresenter>();
        Prefab = prefabs;
    }

    public void Spawn()
    {
        var emptyPoints = _targetPoints.FindAll(point => point.isEmpty);
        var activePallets = YG2.saves.shopModel.upgradeItems.Count(item => item.Type == TypeShopItem.ShipUpgrade && item.IsPurchased);

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

    public void ChangeShips(List<BaseShipView> ships)
    {
        Prefab = ships;
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
