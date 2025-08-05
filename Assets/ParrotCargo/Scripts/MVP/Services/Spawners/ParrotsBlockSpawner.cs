using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Zenject;
using System;

public class ParrotsBlockSpawner : BaseSpawner<ParrotsBlockView>
{
    //_xIncrement = 26f;
    //[SerializeField] private int _activeObjectsMaxCount = 3;
    [SerializeField] private int _ySpawnOffset = 3;

    private DiContainer _container;
    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public ReactiveCommand RespawnBlocks = new ReactiveCommand();

    public List<ParrotBlockPresenter> Spawn()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();

        foreach (Transform spawnPoint in SpawnPoints)
        {
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y + _ySpawnOffset, spawnPoint.position.z);
            SpawnPoint point = spawnPoint.GetComponent<SpawnPoint>();
            point.GetBirds();

            var parrotBlockView = SpawnObject(spawnPosition);
            parrotBlockView.Initialize();
            var parrotBlock = new ParrotBlock(parrotBlockView.GetComponent<Transform>());
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView);
            parrotBlockPresenter.Initialize();

            parrotBlockPresenter.ChangingActive.Subscribe(presenter =>
            {
                Release(parrotBlockView);
                point.GiveAwayBirds();
                RespawnBlocks.Execute();
                //parrotBlockPresenter.Dispose();
            });

            _parrotBlockPresenters.Add(parrotBlockPresenter);
        }

        return _parrotBlockPresenters;
    }

    protected override void CreatePool()
    {
        if (Pool == null)
            Pool = new BasePool<ParrotsBlockView>(ObjectsMaxCount, Parent, _container);
    }

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        CreatePool();
    }
}
