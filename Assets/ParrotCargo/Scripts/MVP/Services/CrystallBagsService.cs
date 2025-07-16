using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Assets.Scripts.MVP.Services.Spawners;

public class CrystallBagsService : BaseService
{
    [SerializeField] private CrystallBagSpawner _crystallBagSpawner;

    private void FixedUpdate()
    {
        _crystallBagSpawner.Spawn();
    }

    public override void Initialize()
    {
        _crystallBagSpawner.Initialize();
        _crystallBagSpawner.CreateObjects();
        _crystallBagSpawner.Spawn();
    }

    private void Start()
    {
        //_crystallBagSpawner.Spawn();
    }
}
