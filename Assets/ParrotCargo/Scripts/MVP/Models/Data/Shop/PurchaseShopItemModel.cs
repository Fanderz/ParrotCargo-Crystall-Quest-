using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class PurchaseShopItemModel : ShopItemModel
{
    [SerializeField] private List<ShopSaveData> _items;

    public int ObjectsCnt => _items.FindAll(item => item.IsPurchased).Count;

    public ReactiveCommand ObjectPurchased = new ReactiveCommand();

    public PurchaseShopItemModel(List<ShopSaveData> items, TypeShopItem itemType)
    {
        _items = items;
        ItemType = itemType;
    }
}
