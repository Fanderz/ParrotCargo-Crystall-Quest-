using System;
using UniRx;
using UnityEngine;
using YG;

[Serializable]
public class UpgradeShopItemModel : ShopItemModel
{
    private int _objectsCnt;

    public int ObjectsCnt => _objectsCnt;

    public UpgradeShopItemModel(int objectsCnt)
    {
        _objectsCnt = objectsCnt;
    }

    public void AddObject()
    {
        _objectsCnt += 1;
    }
}
