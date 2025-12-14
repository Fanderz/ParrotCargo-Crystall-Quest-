using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using YG;

public class ShopPresenter
{
    private ShopModel _model;
    private ShopView _view;
    private CoinsModel _wallet;

    private List<ShopItemPresenter> _shopItemPresenters;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();
    public ShopModel ShopModel => _model;

    public ShopPresenter(ShopModel model, ShopView view, List<ShopItem> shopItems)
    {
        _model = model;
        _view = view;
        _view.Initialize(shopItems);
        _wallet = YG2.saves.coinsProgress;

        _shopItemPresenters = new List<ShopItemPresenter>();
    }

    public void Initialize()
    {
        for (int i = 0; i < _view.ShopItems.Count; i++)
        {
            if (_view.ShopItems[i] is UpgradesShopItem && _model.UpgradeItems[i] is UpgradeShopItemModel)
            {
                UpgradesShopItem upgradesShopItem = (UpgradesShopItem)_view.ShopItems[i];
                UpgradeShopItemModel upgradeShopItemModel = (UpgradeShopItemModel)_model.UpgradeItems[i];

                ShopItemPresenter shopItemPresenter = new ShopItemPresenter(upgradesShopItem, upgradeShopItemModel);
                shopItemPresenter.Initialize();
                shopItemPresenter.TryPurchase.Subscribe(subItem => TryPurchase(subItem));

                _shopItemPresenters.Add(shopItemPresenter);
            }
        }
    }

    private void TryPurchase(UpgradeShopSubItem subItem)
    {
        if(CanPurchase(subItem.Price))
        {
            subItem.OnPurchase();
            PurchaseCommand?.Execute(subItem.Price);
        }
    }

    private bool CanPurchase(int price)
    {
        return price <= _wallet.Value;
    }
}
