using System.Collections.Generic;
using UnityEngine;

using UniRx;
using Zenject;

public class ParrotsBlockSpawner : BaseSpawner<ParrotsBlockView>
{
    [SerializeField] private int _ySpawnOffset = 3;

    [Header("Wawe Settings")]
    [SerializeField] private int _maxTripleBlocksOnOneTempPallet = 2;
    [SerializeField] private int _maxDoubleBlocksOnOneTempPallet = 2;

    [SerializeField] private SkinService _skinService;

    private int _selectedTripleBlocks = 0;
    private int _selectedDoubleBlocks = 0;

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

    public List<ParrotBlockPresenter> Spawn(int tempPalletsCount, int freeShipPalletsCount)
    {
        _selectedTripleBlocks = 0;
        _selectedDoubleBlocks = 0;

        _parrotBlockPresenters = new List<ParrotBlockPresenter>();

        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            List<ParrotsBlockView> allowedPrefabs = Prefab;

            Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + _ySpawnOffset, spawnPoint.transform.position.z);
            spawnPoint.GetBirds();

            if (tempPalletsCount == 1)
                allowedPrefabs = GetAllowedPrefabs();

            var selectedPrefab = GetRandomAllowedPrefab(allowedPrefabs);

            var parrotBlockView = SpawnObject(spawnPosition, null, selectedPrefab);

            //if (parrotBlockView == null)
            //{
            //    Debug.Log("ParrotBlockView is null");
            //    continue;
            //}

            parrotBlockView.Initialize(_skinService.CurrentBird.PrefabBird);
            var parrotBlock = new ParrotBlock(parrotBlockView.GetComponent<Transform>());
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView);
            parrotBlockPresenter.Initialize();

            parrotBlockPresenter.ChangingActiveCommand.Subscribe(presenter => { Release(parrotBlockView); });
            parrotBlockPresenter.PickedBagsCommand.Subscribe(block => { spawnPoint.GiveAwayBirds(); });

            _parrotBlockPresenters.Add(parrotBlockPresenter);
        }

        return _parrotBlockPresenters;
    }

    protected override void CreatePool()
    {
        if (Pool == null)
            Pool = new PoolParrotsBlock(ObjectsMaxCount, Parent, _container);
    }

    private List<ParrotsBlockView> GetAllowedPrefabs()
    {
        List<ParrotsBlockView> allowedPrefabs = new List<ParrotsBlockView>();

        foreach (ParrotsBlockView prefab in Prefab)
        {
            TypeParrotsBlock type = prefab.TypeParrotsBlock;

            bool isTripleBlock = type == TypeParrotsBlock.ThreeRL || type == TypeParrotsBlock.TreeLR;
            bool isDoubleBlock = type == TypeParrotsBlock.TwoLR || type == TypeParrotsBlock.TwoRL || type == TypeParrotsBlock.TwoLine;

            if (isTripleBlock && _selectedTripleBlocks >= _maxTripleBlocksOnOneTempPallet)
                continue;

            if (isDoubleBlock && _selectedDoubleBlocks >= _maxDoubleBlocksOnOneTempPallet)
                continue;

            allowedPrefabs.Add(prefab);
        }

        if (allowedPrefabs.Count == 0)
            allowedPrefabs = Prefab;

        return allowedPrefabs;
    }

    private ParrotsBlockView GetRandomAllowedPrefab(List<ParrotsBlockView> prefabs)
    {
        int randomIndex = Random.Range(0, prefabs.Count);

        UpdateWaveCounters(prefabs[randomIndex].TypeParrotsBlock);

        return prefabs[randomIndex];
    }

    private void UpdateWaveCounters(TypeParrotsBlock type)
    {
        if (type == TypeParrotsBlock.ThreeRL || type == TypeParrotsBlock.TreeLR)
            _selectedTripleBlocks++;

        if (type == TypeParrotsBlock.TwoLR || type == TypeParrotsBlock.TwoRL || type == TypeParrotsBlock.TwoLine)
            _selectedDoubleBlocks++;
    }

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        CreatePool();
    }
}
