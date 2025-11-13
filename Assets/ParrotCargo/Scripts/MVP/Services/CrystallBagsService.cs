using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Assets.Scripts.MVP.Services.Spawners;
using UniRx;

public class CrystallBagsService : BaseService
{
    [SerializeField] private CrystallBagSpawner _crystallBagSpawner;

    [Inject] PlayerProgressService _playerProgressService;

    public override void Initialize()
    {
        _crystallBagSpawner.Initialize();
        _crystallBagSpawner.CreateObjects();
        _crystallBagSpawner.Spawn();

        foreach (CrystallBagPresenter presenter in _crystallBagSpawner.CrystallBags)
            presenter.BagReleased.Subscribe(released => { _playerProgressService.IncreaseValuesOnBagRelease(); });
    }
}
