using UnityEngine;

using Assets.Scripts.MVP.Services.Spawners;

using Zenject;
using UniRx;

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
        Debug.Log("<size=50>Вызвался CrystallBagsService.OnStartGame</size>");
        _crystallBagSpawner.Spawn();

        foreach (CrystallBagPresenter presenter in _crystallBagSpawner.CrystallBags)
            presenter.BagReleased.Subscribe(released => { _playerProgressService.IncreaseValuesOnBagRelease(); });
    }
}
