using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

[Serializable]
public class ShopModel
{
    private ShopSaveModel _shopSaveModel;

    public int TempPalletsCnt => _shopSaveModel.upgradeItems.Count(x => x.Type == TypeShopItem.PalletUpgrade && x.IsPurchased);
    public int ShipPalletsCnt => _shopSaveModel.upgradeItems.Count(x => x.Type == TypeShopItem.ShipUpgrade && x.IsPurchased);

    //public IReadOnlyList<UpgradeShopItemModel> UpgradeItems => _shopSaveModel.upgradeItems;

    public ReactiveCommand<TypeShopItem> UpgradeItemChanged = new ReactiveCommand<TypeShopItem>();
    public ReactiveCommand<List<ParrotsBlockView>> ParrotBlockViewsChanged = new ReactiveCommand<List<ParrotsBlockView>>();
    public ReactiveCommand<BaseShipView> ShipViewsChanged = new ReactiveCommand<BaseShipView>();

    public ShopModel(ShopSaveModel save)
    {
        _shopSaveModel = save;
    }

    public bool CanPurchaseUpgrade(TypeShopItem type)
    {
        return _shopSaveModel.upgradeItems.Any(x => x.Type == type && !x.IsPurchased);
    }

    public bool PurchaseUpgrade(TypeShopItem type)
    {
        var item = _shopSaveModel.upgradeItems.FirstOrDefault(x => x.Type == type && !x.IsPurchased);

        if (item == null)
            return false;

        item.IsPurchased = true;
        UpgradeItemChanged.Execute(type);
        return true;
    }
}
