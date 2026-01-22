using System.Collections.Generic;

using UnityEngine;

using Zenject;
using UniRx;
using System.Linq;

public class ShipsService : BaseService
{
    [SerializeField] private ShipsSpawner _shipSpawner;

    private int _currentShipUpgrades;

    [Inject] private ShopService _shopService;
    [Inject] private SkinService _skinService;

    public IReadOnlyList<ShipPresenter> Ships => _shipSpawner.ShipPresenters;

    public override void Initialize()
    {
    }

    public void StartGame()
    {
        _shipSpawner.Initialize(_skinService.CurrentShip.ShipPrefabs.ToList());
        _shipSpawner.CreateObjects();
        _shipSpawner.Spawn();
        _currentShipUpgrades = _shopService.Model.ShipPalletsCnt;
        ApplyUpgrades(_currentShipUpgrades);
    }

    private void ApplyUpgrades(int count)
    {
        for (int i = 0; i < count; i++)
            foreach (ShipPresenter shipPresenter in _shipSpawner.ShipPresenters)
                shipPresenter.ActivatePallet();
    }

}
