using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

[Serializable]
public class ShopModel
{
    private List<UpgradeShopItemModel> _upgradeItems;
    private List<ParrotsBlockView> _parrotBlockViews;
    private List<BaseShipView> _shipViews;

    public int TempPalletsCnt => _upgradeItems.First(upgradeItem => upgradeItem.ItemType == TypeShopItem.PalletUpgrade).ObjectsCnt;
    public int ShipPalletsCnt => _upgradeItems.First(upgradeItem => upgradeItem.ItemType == TypeShopItem.ShipUpgrade).ObjectsCnt;
    public IReadOnlyList<UpgradeShopItemModel> UpgradeItems => _upgradeItems;


    public ReactiveCommand UpgradeItemChanged = new ReactiveCommand();
    public ReactiveCommand<List<ParrotsBlockView>> ParrotBlockViewsChanged = new ReactiveCommand<List<ParrotsBlockView>>();
    public ReactiveCommand<BaseShipView> ShipViewsChanged = new ReactiveCommand<BaseShipView>();

    public ShopModel(List<UpgradeShopItemModel> upgradeItems, List<BaseShipView> shipViews)
    {
        _upgradeItems = upgradeItems;
        _shipViews = shipViews;
    }

    public void OnSave(UpgradeShopItemModel model)
    {
        UpgradeShopItemModel item = _upgradeItems.Find(item => item.ItemType == model.ItemType);

        if (item == null)
            return;

        item = model;
    }

    //public void SetShipView(List<BaseShipView> shipViews)
    //{
    //    _shipViews = shipViews.ToList();
    //}
}
