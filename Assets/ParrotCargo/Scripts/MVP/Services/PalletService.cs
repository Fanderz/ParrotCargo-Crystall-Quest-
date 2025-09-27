using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PalletService : BaseService
{
    [SerializeField] private PalletsSpawner _palletSpawner;

    //[Inject] private ShopService _settings;

    public IReadOnlyList<PalletPresenter> Pallets => _palletSpawner.PalletPresenters;

    public override void Initialize()
    {
        _palletSpawner.Initialize();
        _palletSpawner.Spawn();
    }
}
