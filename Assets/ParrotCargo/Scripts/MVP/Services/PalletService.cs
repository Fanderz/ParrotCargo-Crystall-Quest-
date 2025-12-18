using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;
using UniRx;
using YG;

public class PalletService : BaseService
{
    [SerializeField] private PalletsSpawner _palletSpawner;

    private UpgradeShopItemModel _palletShopItemModel; 

    [Inject] private ShopService _shopSettings;

    public IReadOnlyList<PalletPresenter> Pallets => _palletSpawner.PalletPresenters;

    public override void Initialize()
    {
        _palletShopItemModel = YG2.saves.shopModel.UpgradeItems.First(item => item.ItemType == TypeShopItem.PalletUpgrade);
        _palletSpawner.Initialize();
        _palletSpawner.Spawn(_palletShopItemModel.ObjectsCnt);
    }

    public void OnSave()
    {
        YG2.saves.shopModel.OnSave(_palletShopItemModel);
    }

    public void OnPalletUpgrade()
    {
        _palletSpawner.Spawn(1);
    }
}
