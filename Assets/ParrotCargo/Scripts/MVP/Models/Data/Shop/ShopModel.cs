using System;
using System.Linq;

using UniRx;

[Serializable]
public class ShopModel
{
    private ShopSaveModel _shopSaveModel;

    public int TempPalletsCnt => _shopSaveModel.upgradeItems.Count(x => x.Type == TypeShopItem.PalletUpgrade && x.IsPurchased);
    public int ShipPalletsCnt => _shopSaveModel.upgradeItems.Count(x => x.Type == TypeShopItem.ShipUpgrade && x.IsPurchased);

    public ReactiveCommand<ShopSaveData> ModelChanged = new ReactiveCommand<ShopSaveData>();
    public ReactiveCommand<ShopSaveData> PurchaseItemChanged = new ReactiveCommand<ShopSaveData>();
    public ReactiveCommand PurchaseItemActivated = new ReactiveCommand();

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

        if (item.Type == TypeShopItem.PalletUpgrade || item.Type == TypeShopItem.ShipUpgrade)
        {
            item.isActive = true;
            ModelChanged.Execute(data);
        }
        else
        {
            ModelChanged.Execute(data);
        }

        return true;
    }

    public void ActivatePurchase(ShopSaveData data)
    {
        var item = _shopSaveModel.purchaseItems.FirstOrDefault(model => model == data);
        item.isActive = true;
        PurchaseItemActivated.Execute();
    }
}
