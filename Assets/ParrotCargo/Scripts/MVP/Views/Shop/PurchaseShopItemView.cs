using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;

public class PurchaseShopItemView : ShopItemView
{
    private List<PurchaseShopSubItemView> _subItems;

    public IReadOnlyList<PurchaseShopSubItemView> SubItems => _subItems;

    public ReactiveCommand<PurchaseShopSubItemView> TryPurchase = new ReactiveCommand<PurchaseShopSubItemView>();

    public override void Initialize(ShopItemValues values)
    {
        base.Initialize(values);
        _subItems = new List<PurchaseShopSubItemView>();
    }

    //public override void SetPurchasedOnLoad(ShopSubItemView view)
    //{
    //    //_subItems.Find(subItem => subItem == (PurchaseShopSubItemView)view);

    //    //if (view is PurchaseShopSubItemView purchaseSubItemView && _subItems.Exists(subItem => subItem == purchaseSubItemView))
    //    //    purchaseSubItemView.SetPurchased();
    //}

    public PurchaseShopSubItemView CreateSubItem(ShopSubItemView prefab, int price)
    {
        PurchaseShopSubItemView subItem = Instantiate((PurchaseShopSubItemView)prefab, grid.transform);
        subItem.Initialize(price);
        subItem.TryPurchase.Subscribe(clicked => TryPurchase.Execute((PurchaseShopSubItemView)clicked));

        _subItems.Add(subItem);

        return subItem;
    }
}
