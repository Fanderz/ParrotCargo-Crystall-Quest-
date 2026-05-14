using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;
using UniRx;

public class ParrotsBlockService : BaseService
{
    [SerializeField] private int _multiplierStartCreatedObjects;
    [SerializeField] private ParrotsBlockSpawner _parrotsBlockSpawner;
    [SerializeField] private List<SpawnPoint> _spawnPlatforms;

    [Inject] private ShipsService _shipsService;
    [Inject] private PalletService _palletsService;
    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private AudioService _audioService;
    [Inject] private LevelsService _levelsService;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public ReactiveCommand ParrotsRespawned = new ReactiveCommand();

    public ParrotsBlockSpawner ParrotsBlockSpawner => _parrotsBlockSpawner;

    public override void Initialize()
    {
        for (int i = 0; i < _multiplierStartCreatedObjects; i++)
            _parrotsBlockSpawner.CreateObjects();

        _parrotsBlockSpawner.RespawnBlocks.Subscribe(respawn =>
        {
            if (_spawnPlatforms.TrueForAll(platform => platform.haveBirds == false))
                CreateBlocks();
        });
    }

    public void OnStartGame()
    {
        CreateBlocks();
    }

    private void CreateBlocks()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();
        int tempPalletsCount = _palletsService.Pallets.Count;
        int freeShipPalletsCount = _shipsService.Ships.Sum(ship => ship.EmptyPalletsCnt);

        _parrotBlockPresenters = _parrotsBlockSpawner.Spawn(tempPalletsCount, freeShipPalletsCount);

        _parrotBlockPresenters.ForEach(block =>
        {
            block.GameOverCommand.Subscribe(bl =>
            {
                _parrotBlockPresenters.ForEach(blockPresenter => blockPresenter.DeactivateParrotsWithoutTargetPallet());
                _playerProgressService.OnGameOver();
            });
            block.DroppedBagCommand.Subscribe(crystallBag =>
            {
                _audioService.OnBagDroppedSound();

                if (_levelsService.CurrentTypeGame == TypeGame.LevelsTypeGame)
                    _levelsService.LevelsProgressPresenter.AddCountBagCollected(crystallBag);
            });
        });

        UpdateTargets();
    }

    private void UpdateTargets()
    {
        _parrotBlockPresenters.ForEach(block =>
        {
            block.SetShipTargets(_shipsService.Ships);
            block.SetPalletTargets(_palletsService.Pallets);
        });
    }
}