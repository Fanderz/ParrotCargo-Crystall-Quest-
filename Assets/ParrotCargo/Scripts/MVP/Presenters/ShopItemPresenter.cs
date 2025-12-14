using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShopItemPresenter
{
    private ShopItem _view;
    private ShopItemModel _model;

    public ReactiveCommand<UpgradeShopSubItem> TryPurchase = new ReactiveCommand<UpgradeShopSubItem>();

    public ShopItemPresenter(ShopItem view, ShopItemModel model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        if(_view is UpgradesShopItem && _model is UpgradeShopItemModel)
        {
            UpgradesShopItem upgradeItem = (UpgradesShopItem)_view;
            UpgradeShopItemModel upgradeItemModel = (UpgradeShopItemModel)_model;

            upgradeItem.SetStarsFilledOnLoad(upgradeItemModel.ObjectsCnt);
            upgradeItem.TryPurchase.Subscribe(subItem => TryPurchase.Execute(subItem));

            foreach (UpgradeShopSubItem subItem in upgradeItem.SubItems)
                subItem.StarFilledCommand.Subscribe(cmd => upgradeItemModel.AddObject());
        }
    }

    public void OnSuccessPurchase()
    {

    }
}
