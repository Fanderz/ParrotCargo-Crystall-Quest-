using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class ShipsService : BaseService
{
    [SerializeField] private ShipsSpawner _shipSpawner;

    [Inject] private ShopService _shopService;

    public IReadOnlyList<ShipPresenter> Ships => _shipSpawner.ShipPresenters;

    public ReactiveCommand OnShipsChanged = new ReactiveCommand();

    public override void Initialize()
    {
        _shipSpawner.Initialize();
        _shipSpawner.CreateObjects();
        _shipSpawner.Spawn();
    }
}
