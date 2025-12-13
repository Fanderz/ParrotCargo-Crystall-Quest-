using System;
using UniRx;
using UnityEngine;
using YG;

[Serializable]
public class UpgradeShopItemModel : ShopItemModel
{
    private int _objectsCnt;

    //public ReactiveCommand<int> CountChanged = new ReactiveCommand<int>();
    public int ObjectsCnt => _objectsCnt;

    public UpgradeShopItemModel(int objectsCnt)
    {
        _objectsCnt = objectsCnt;
    }

    public void SetTempPalletsCount()
    {
        _objectsCnt += 1;
        //CountChanged.Execute(_objectsCnt);
        YG2.SaveProgress();
    }
}
