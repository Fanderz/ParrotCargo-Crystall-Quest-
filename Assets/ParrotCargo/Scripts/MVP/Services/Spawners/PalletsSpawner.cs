using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PalletsSpawner : BaseSpawner<PalletView>
{
    [SerializeField] private float _incrementX;

    private float _xOffset = 0f;
    private DiContainer _container;
    private List<PalletPresenter> _palletPresenters;

    public IReadOnlyList<PalletPresenter> PalletPresenters => _palletPresenters;

    public void Initialize()
    {
        _palletPresenters = new List<PalletPresenter>();
    }

    public void Spawn()
    {
        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            Vector3 spawnPosition = new Vector3(SpawnPoints[0].position.x + _xOffset, SpawnPoints[0].position.y, SpawnPoints[0].position.z);

            var palletView = SpawnObject(spawnPosition);
            var pallet = new Pallet();
            var palletPresenter = new PalletPresenter(palletView, pallet);

            _palletPresenters.Add(palletPresenter);

            IncreaseOffset(ref _xOffset, _incrementX);
        }
    }

    protected override void CreatePool()
    {
        if (Pool == null)
            Pool = new BasePool<PalletView>(ObjectsMaxCount, Parent, _container);
    }

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        CreatePool();
    }

    private void IncreaseOffset(ref float offset, float increment)
    {
        offset += increment;
    }
}
