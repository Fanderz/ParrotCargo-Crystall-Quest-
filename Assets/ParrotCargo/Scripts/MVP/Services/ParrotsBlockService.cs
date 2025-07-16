using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class ParrotsBlockService : BaseService
{
    [SerializeField] private ParrotsBlockSpawner _parrotsBlockSpawner;
    [SerializeField] private List<SpawnPoint> _spawnPlatforms;

    [Inject] private ShipsService _shipsService;
    [Inject] private PalletService _palletsService;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public ReactiveCommand ParrotsRespawned = new ReactiveCommand();

    public ParrotsBlockSpawner ParrotsBlockSpawner => _parrotsBlockSpawner;

    private void FixedUpdate()
    {
        if (_parrotBlockPresenters.TrueForAll(presenter => presenter.IsBlockReleased == true) && _spawnPlatforms.TrueForAll(platform => platform.haveBirds == false))
            CreateBlocks();
    }

    public override void Initialize()
    {
        _parrotsBlockSpawner.CreateObjects();
        CreateBlocks();
        _shipsService.OnShipsChanged.Subscribe(changed => { UpdateTargets(); });

        _parrotsBlockSpawner.RespawnBlocks.Subscribe(respawn => { TryRespawnBlocks(); });
    }

    private void CreateBlocks()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();
        _parrotBlockPresenters = _parrotsBlockSpawner.Spawn();
        UpdateTargets();
    }

    private void TryRespawnBlocks()
    {
        if (_parrotBlockPresenters.TrueForAll(presenter => presenter.IsBlockReleased == true) && _spawnPlatforms.TrueForAll(platform => platform.haveBirds == false))
        {
            CreateBlocks();
            ParrotsRespawned.Execute();
        }
    }

    private void UpdateTargets()
    {
        foreach (var parrotBlock in _parrotBlockPresenters)
        {
            parrotBlock.SetShipTargets(_shipsService.Ships);
            parrotBlock.SetPalletTargets(_palletsService.Pallets);
        }
    }
}
