using System.Collections.Generic;

using UnityEngine;

using Zenject;
using UniRx;
using System.Linq;

public class ShipsService : BaseService
{
    [SerializeField] private int _multiplierStartCreatedObjects;
    [SerializeField] private ShipsSpawner _shipSpawner;

    [Inject] private ShopService _shopService;
    [Inject] private SkinService _skinService;
    [Inject] private AudioService _audioService;

    public IReadOnlyList<ShipPresenter> Ships => _shipSpawner.ShipPresenters;

    public override void Initialize()
    {
    }

    public void OnStartGame()
    {
        _shipSpawner.Initialize(_skinService.CurrentShip.ShipPrefabs.ToList());

        for (int i = 0; i < _multiplierStartCreatedObjects; i++)
            _shipSpawner.CreateObjects();

        _shipSpawner.Spawn();

        Ships.ToList().ForEach(ship => ship.PlayAudio.Subscribe(state => _audioService.OnShipStateChangedSound()));
    }

    private void ApplyUpgrades(int count)
    {
        for (int i = 0; i < count; i++)
            foreach (ShipPresenter shipPresenter in _shipSpawner.ShipPresenters)
                shipPresenter.AddPallet();
    }
}
