using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class ShipsService : BaseService
{
    [SerializeField] private ShipsSpawner _shipSpawner;

    public IReadOnlyList<ShipPresenter> Ships => _shipSpawner.ShipPresenters;

    public ReactiveCommand OnShipsChanged = new ReactiveCommand();

    public override void Initialize()
    {
        _shipSpawner.Initialize();
        _shipSpawner.Spawn();
    }
}
