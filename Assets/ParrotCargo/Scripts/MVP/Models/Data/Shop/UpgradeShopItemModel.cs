using System;

using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class UpgradeShopItemModel : ShopItemModel
{
    [SerializeField] private List<ShopSaveData> _items;

    public int ObjectsCnt => _items.FindAll(item => item.IsPurchased).Count;

    public ReactiveCommand ObjectsCntChanged = new ReactiveCommand();

    public UpgradeShopItemModel(List<ShopSaveData> items, TypeShopItem itemType)
    {
        _items = items;
        ItemType = itemType;
    }

    public void AddObject()
    {
        _items.First(item => item.IsPurchased == false).IsPurchased = true;
        ObjectsCntChanged.Execute();
    }
}
