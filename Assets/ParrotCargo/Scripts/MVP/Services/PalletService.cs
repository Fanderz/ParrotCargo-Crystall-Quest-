using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PalletService : BaseService
{
    [SerializeField] private PalletsSpawner _palletSpawner;

    public IReadOnlyList<PalletPresenter> Pallets => _palletSpawner.PalletPresenters;

    //public ReactiveCommand OnShipsChanged = new ReactiveCommand();

    public override void Initialize()
    {
        _palletSpawner.Initialize();
        _palletSpawner.Spawn();
    }
}
