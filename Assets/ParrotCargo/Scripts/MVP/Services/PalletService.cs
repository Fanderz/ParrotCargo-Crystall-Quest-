using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;
using UniRx;
using YG;

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
        _currentPalletsCnt = _shopService.Model.TempPalletsCnt;

        _palletSpawner.Initialize();
        _palletSpawner.Spawn(_currentPalletsCnt);
    }
}
