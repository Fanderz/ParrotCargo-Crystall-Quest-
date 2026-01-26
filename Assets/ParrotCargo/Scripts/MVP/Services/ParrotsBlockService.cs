using System.Collections.Generic;

using UnityEngine;

using Zenject;
using UniRx;

public class ParrotsBlockService : BaseService
{
    [SerializeField] private ParrotsBlockSpawner _parrotsBlockSpawner;
    [SerializeField] private List<SpawnPoint> _spawnPlatforms;

    [Inject] private ShipsService _shipsService;
    [Inject] private PalletService _palletsService;
    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private AudioService _audioService;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public ReactiveCommand ParrotsRespawned = new ReactiveCommand();

    public ParrotsBlockSpawner ParrotsBlockSpawner => _parrotsBlockSpawner;

    public override void Initialize()
    {
        _parrotsBlockSpawner.RespawnBlocks.Subscribe(respawn =>
        {
            if (_spawnPlatforms.TrueForAll(platform => platform.haveBirds == false))
                CreateBlocks();
        });
    }

    public void StartGame()
    {
        _parrotsBlockSpawner.CreateObjects();
        CreateBlocks();
    }

    private void CreateBlocks()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();
        _parrotBlockPresenters = _parrotsBlockSpawner.Spawn();

        _parrotBlockPresenters.ForEach(block =>
        {
            block.GameOverCommand.Subscribe(bl => _playerProgressService.OnGameOver());
            block.ParrotDroppedBagSoundCommand.Subscribe(bl => _audioService.OnBagDroppedSound());
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
