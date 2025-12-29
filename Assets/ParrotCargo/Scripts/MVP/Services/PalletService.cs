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
        _palletSpawner.Initialize();

        _currentPalletsCnt = _shopService.Model.TempPalletsCnt;
        _palletSpawner.Spawn(_currentPalletsCnt);

        _shopService.Model.ModelChanged.Subscribe(OnUpgradeChanged).AddTo(this);
    }

    private void OnUpgradeChanged(ShopSaveData data)
    {
        if (data.Type != TypeShopItem.PalletUpgrade)
            return;

        int newCnt = _shopService.Model.TempPalletsCnt;
        int delta = newCnt - _currentPalletsCnt;

        if (delta > 0)
            _palletSpawner.Spawn(delta);

        _currentPalletsCnt = newCnt;
    }
}
