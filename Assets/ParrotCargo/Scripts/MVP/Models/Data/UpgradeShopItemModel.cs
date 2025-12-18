using System;

using UnityEngine;
using UniRx;

[Serializable]
public class UpgradeShopItemModel : ShopItemModel
{
    [SerializeField] private int _objectsCnt;

    public int ObjectsCnt => _objectsCnt;

    public ReactiveCommand ObjectsCntChanged = new ReactiveCommand();

    public UpgradeShopItemModel(int objectsCnt, TypeShopItem itemType)
    {
        _objectsCnt = objectsCnt;
        ItemType = itemType;
    }

    public void AddObject()
    {
        _objectsCnt++;
        ObjectsCntChanged.Execute();
    }
}
