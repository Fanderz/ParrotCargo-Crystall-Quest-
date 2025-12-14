using System.Collections.Generic;
using UniRx;
using UnityEngine;
using YG;
using Zenject;

public class PalletService : BaseService
{
    [SerializeField] private PalletsSpawner _palletSpawner;

    private UpgradeShopItemModel _palletShopItemModel; 

    [Inject] private ShopService _shopSettings;

    public IReadOnlyList<PalletPresenter> Pallets => _palletSpawner.PalletPresenters;

    public override void Initialize()
    {
        _palletShopItemModel = YG2.saves.shopModel.UpgradeItems[0];
        _palletSpawner.Initialize();
        _palletSpawner.Spawn(_palletShopItemModel.ObjectsCnt);
    }

    public void OnSave()
    {
        YG2.saves.shopModel.SetTempPalletsOnSave(_palletShopItemModel);
    }
}
