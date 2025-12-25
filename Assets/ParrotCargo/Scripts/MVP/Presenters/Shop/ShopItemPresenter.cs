using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShopItemPresenter
{
    private readonly ShopItemView _view;
    private readonly List<ShopSaveData> _model;
    private readonly TypeShopItem _type;

    public ReactiveCommand<ShopSubItemView> TryPurchase = new ReactiveCommand<ShopSubItemView>();

    public ShopItemPresenter(ShopItemView view, List<ShopSaveData> model)
    {
        _view = view;
        _model = model;
        _type = view.ItemType;
    }

    public TypeShopItem ItemType => _type;

    public void Initialize(ShopItemValues values)
    {
        _view.Initialize(values);

        if (_view is UpgradesShopItemView upgradesItemView)
        {
            foreach (var item in _model)
            {
                UpgradeShopSubItemView subItem = upgradesItemView.CreateSubItem(values.ChildItemPrefab, values.Price);
                ShopSubItemPresenter subItemPresenter = new ShopSubItemPresenter(subItem, item);
                subItemPresenter.Initialize();
            }

            upgradesItemView.TryPurchase.Subscribe(subItem => TryPurchase.Execute(subItem));
        }

        if(_view is PurchaseShopItemView purchaseItemView)
        {
            foreach (var item in _model)
            {
                PurchaseShopSubItemView subItem = purchaseItemView.CreateSubItem(values.ChildItemPrefab, values.Price);
                ShopSubItemPresenter subItemPresenter = new ShopSubItemPresenter(subItem, item);
                subItemPresenter.Initialize();
            }
        }    
    }

    public void OnModelChanged(int newPurchasedCount)
    {
        _view.SetPurchasedOnLoad(newPurchasedCount);
    }
}
