using System.Collections.Generic;

using UnityEngine;

using Zenject;

public class PalletService : BaseService
{
    [SerializeField] private PalletsSpawner _palletSpawner;

    [Inject] private ShopService _shopService;

    public IReadOnlyList<PalletPresenter> Pallets => _palletSpawner.PalletPresenters;

    private int _currentPalletsCnt;

    public override void Initialize()
    {
    }

    public void OnStartGame()
    {
        if (_shopService.Model == null)
            _shopService.Initialize();

        _currentPalletsCnt = _shopService.Model.TempPalletsCnt;

        _palletSpawner.Initialize();
        _palletSpawner.Spawn(_currentPalletsCnt);
    }
}
