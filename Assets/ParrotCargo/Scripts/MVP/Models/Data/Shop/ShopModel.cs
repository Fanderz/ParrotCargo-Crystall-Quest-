using System;
using System.Linq;

using UniRx;
using YG;

[Serializable]
public class ShopModel
{
    private ShopSaveModel _shopSaveModel;

    public int TempPalletsCnt => _shopSaveModel.upgradeItems.Count(x => x.Type == TypeShopItem.PalletUpgrade && x.IsPurchased);
    public int ShipPalletsCnt => _shopSaveModel.upgradeItems.Count(x => x.Type == TypeShopItem.ShipUpgrade && x.IsPurchased);

    public ReactiveCommand<ShopSaveData> UpgradeChanged = new ReactiveCommand<ShopSaveData>();
    public ReactiveCommand<(int, TypeShopItem)> SkinChanged = new ReactiveCommand<(int, TypeShopItem)>();

    public ShopModel(ShopSaveModel save)
    {
        _shopSaveModel = save;
    }

    public bool Purchase(ShopSaveData data)
    {
        ShopSaveData item;

        if (data is NullableShopSaveData)
            return false;

        if (data.Type == TypeShopItem.PalletUpgrade || data.Type == TypeShopItem.ShipUpgrade)
            item = _shopSaveModel.upgradeItems.FirstOrDefault(finded => finded == data && finded.IsPurchased == false);
        else
            item = _shopSaveModel.purchaseItems.FirstOrDefault(finded => finded == data && finded.IsPurchased == false);

        item.IsPurchased = true;
        UpgradeChanged.Execute(data);

        return true;
    }

    public void ActivatePurchase(ShopSaveData data)
    {
        var item = _shopSaveModel.purchaseItems.FirstOrDefault(model => model == data);

        if (item != null)
            _shopSaveModel.purchaseItems.Where(purchaseItem => purchaseItem.Type == item.Type).ToList().ForEach(purchaseItem => purchaseItem.isActive = false);

        item.isActive = true;

        SkinChanged.Execute((_shopSaveModel.purchaseItems.Where(purchaseItem => purchaseItem.Type == item.Type).ToList().IndexOf(item), item.Type));
    }
}
