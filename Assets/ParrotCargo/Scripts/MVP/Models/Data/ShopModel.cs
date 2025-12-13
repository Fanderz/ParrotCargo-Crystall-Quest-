using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using YG;

[Serializable]
public class ShopModel
{
    private List<UpgradeShopItemModel> _upgradeItems;
    private List<ParrotsBlockView> _parrotBlockViews;
    private List<BaseShipView> _shipViews;

    //public IReadOnlyList<ParrotsBlockView> ParrotBlockViews => _parrotBlockViews;
    public List<BaseShipView> ShipViews => _shipViews;
    public int TempPalletsCnt => _upgradeItems[0].ObjectsCnt;

    public ReactiveCommand UpgradeItemChanged = new ReactiveCommand();

    public ReactiveCommand<List<ParrotsBlockView>> ParrotBlockViewsChanged = new ReactiveCommand<List<ParrotsBlockView>>();
    public ReactiveCommand<BaseShipView> ShipViewsChanged = new ReactiveCommand<BaseShipView>();

    public ShopModel(List<UpgradeShopItemModel> upgradeItems, List<BaseShipView> shipViews)
    {
        _upgradeItems = upgradeItems;
        //_parrotBlockViews = parrotBlockViews;
        _shipViews = shipViews;
    }

    //public void SetTempPalletsCount()
    //{
    //    _tempPalletsCnt += 1;
    //    TempPalletsCntChanged.Execute(_tempPalletsCnt);
    //    YG2.SaveProgress();
    //}

    //public void SetShipPalletsCount()
    //{
    //    _shipPalletsCnt+=1;
    //    TempPalletsCntChanged.Execute(_shipPalletsCnt);
    //    YG2.SaveProgress();
    //}

    //public void SetParrotsViews(List<ParrotsBlockView> parrotBlockViews)
    //{
    //    _parrotBlockViews = parrotBlockViews.ToList();
    //    ParrotBlockViewsChanged.Execute(_parrotBlockViews);
    //}

    public void SetShipView(List<BaseShipView> shipViews)
    {
        _shipViews = shipViews.ToList();
        //ShipViewsChanged.Execute(_shipView);
    }
}
