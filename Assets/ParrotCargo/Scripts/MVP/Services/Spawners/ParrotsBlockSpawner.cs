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
    private List<SpawnPoint> _spawnPoints;

    public ReactiveCommand RespawnBlocks = new ReactiveCommand();

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>();

        foreach (Transform transform in SpawnPoints)
            _spawnPoints.Add(transform.GetComponent<SpawnPoint>());
    }

    public List<ParrotBlockPresenter> Spawn()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();

        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + _ySpawnOffset, spawnPoint.transform.position.z);
            spawnPoint.GetBirds();

            var parrotBlockView = SpawnObject(spawnPosition);
            parrotBlockView.Initialize();
            var parrotBlock = new ParrotBlock(parrotBlockView.GetComponent<Transform>());
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView);
            parrotBlockPresenter.Initialize();

            parrotBlockPresenter.ChangingActive.Subscribe(presenter =>
            {
                Release(parrotBlockView);

                RespawnBlocks.Execute();
            });

            parrotBlockPresenter.PickedBags.Subscribe(block => { 
                spawnPoint.GiveAwayBirds(); });

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
