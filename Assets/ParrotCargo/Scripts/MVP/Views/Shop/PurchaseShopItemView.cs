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

    public PurchaseShopSubItemView CreateSubItem(ShopSubItemView prefab, int price)
    {
        PurchaseShopSubItemView subItem = Instantiate(prefab, grid.transform).GetComponent<PurchaseShopSubItemView>();
        subItem.Initialize(price);
        subItem.TryPurchase.Subscribe(clicked => TryPurchase.Execute(clicked.GetComponent<PurchaseShopSubItemView>()));

        _subItems.Add(subItem);

        return subItem;
    }
}
