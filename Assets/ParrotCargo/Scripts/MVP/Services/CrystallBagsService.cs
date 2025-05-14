using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.MVP.Services.Spawners;

public class CrystallBagsService : BaseService
{
    [SerializeField] private CrystallBagSpawner _crystallBagSpawner;

    private List<CrystallBagPresenter> _crystallBagPresenters;

    public override void Initialize()
    {
        _crystallBagPresenters = new List<CrystallBagPresenter>();
        _crystallBagPresenters = _crystallBagSpawner.Spawn(Parent);
    }
}
