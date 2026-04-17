using UnityEngine;

using Assets.Scripts.MVP.Services.Spawners;

using Zenject;

public class CrystallBagsService : BaseService
{
    [SerializeField] private CrystallBagSpawner _crystallBagSpawner;

    [Inject] PlayerProgressService _playerProgressService;

    public override void Initialize()
    {
        _crystallBagSpawner.Initialize();
        _crystallBagSpawner.CreateObjects();
    }

    public void OnStartGame()
    {
        _crystallBagSpawner.Spawn();
    }
}
