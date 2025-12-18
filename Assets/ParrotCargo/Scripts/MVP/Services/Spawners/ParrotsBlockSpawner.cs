using System.Collections.Generic;
using UnityEngine;

using UniRx;
using Zenject;

public class ParrotsBlockSpawner : BaseSpawner<ParrotsBlockView>
{
    [SerializeField] private int _ySpawnOffset = 3;
    [SerializeField] private SkinBirdService _skinBirdService;

    private DiContainer _container;
    private List<ParrotBlockPresenter> _parrotBlockPresenters;
    private List<SpawnPoint> _spawnPoints;

    public ReactiveCommand RespawnBlocks = new ReactiveCommand();

    protected override void Awake()
    {
        _spawnPoints = new List<SpawnPoint>();

        foreach (Transform transform in SpawnPoints)
        {
            transform.TryGetComponent(out SpawnPoint spawnPoint);
            spawnPoint.OnSpawnPointEmpty.Subscribe(point => RespawnBlocks.Execute());
            _spawnPoints.Add(spawnPoint);
        }
    }

    public List<ParrotBlockPresenter> Spawn()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();

        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + _ySpawnOffset, spawnPoint.transform.position.z);
            spawnPoint.GetBirds();

            var parrotBlockView = SpawnObject(spawnPosition);
            parrotBlockView.Initialize(_skinBirdService.CurrentBird.PrefabBird);
            var parrotBlock = new ParrotBlock(parrotBlockView.GetComponent<Transform>());
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView);
            parrotBlockPresenter.Initialize();

            parrotBlockPresenter.ChangingActive.Subscribe(presenter =>
            {
                Release(parrotBlockView);
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
            Pool = new PoolParrotsBlock(ObjectsMaxCount, Parent, _container);
    }

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        CreatePool();
    }
}
