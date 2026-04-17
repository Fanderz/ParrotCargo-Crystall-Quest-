using System.Collections.Generic;

using UnityEngine;

using Zenject;
using UniRx;
using System.Linq;

public class ShipsService : BaseService
{
    [SerializeField] private int _multiplierStartCreatedObjects;
    [SerializeField] private ShipsSpawner _shipSpawner;

    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private ShopService _shopService;
    [Inject] private SkinService _skinService;
    [Inject] private AudioService _audioService;
    [Inject] private LevelsService _levelsService;

    public IReadOnlyList<ShipPresenter> Ships => _shipSpawner.ShipPresenters;

    public bool IsAnyShipsGoingToRelease => _shipSpawner.ShipPresenters.Any(ship => ship.isGoingToRelease == true);

    public override void Initialize()
    {
    }

    public void OnStartGame()
    {
        _shipSpawner.Initialize(_skinService.CurrentShip.ShipPrefabs.ToList());

        for (int i = 0; i < _multiplierStartCreatedObjects; i++)
            _shipSpawner.CreateObjects();

        _shipSpawner.Spawn();

        _shipSpawner.ShipStoppedCommand.Subscribe(state => _audioService.OnShipStateChangedSound());
        _shipSpawner.ShipReleasedCommand.Subscribe(bags =>
        {
            _playerProgressService.IncreaseValuesOnBagRelease(bags.Count);

            if (_levelsService.CurrentTypeGame == TypeGame.LevelsTypeGame)
                _levelsService.LevelsProgressPresenter.TryFinishLevel();
        });
    }

    private void ApplyUpgrades(int count)
    {
        for (int i = 0; i < count; i++)
            foreach (ShipPresenter shipPresenter in _shipSpawner.ShipPresenters)
                shipPresenter.AddPallet();
    }

    private void OnDestroy()
    {
        _shipSpawner.ShipStoppedCommand?.Dispose();
        _shipSpawner.ShipReleasedCommand?.Dispose();
    }
}
