using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using YG;

[Serializable]
public class ShopModel
{
    private int _tempPalletsCnt;
    private int _shipPalletsCnt;

    private List<ParrotsBlockView> _parrotBlockViews;
    private List<BaseShipView> _shipViews;

    public int TempPalletsCount => _tempPalletsCnt;
    public int ShipPalletsCount => _shipPalletsCnt;
    //public IReadOnlyList<ParrotsBlockView> ParrotBlockViews => _parrotBlockViews;
    public List<BaseShipView> ShipViews => _shipViews;

    public ReactiveCommand<int> TempPalletsCntChanged = new ReactiveCommand<int>();
    public ReactiveCommand<int> ShipPalletsCntChanged = new ReactiveCommand<int>();
    public ReactiveCommand<List<ParrotsBlockView>> ParrotBlockViewsChanged = new ReactiveCommand<List<ParrotsBlockView>>();
    public ReactiveCommand<BaseShipView> ShipViewsChanged = new ReactiveCommand<BaseShipView>();

    public ShopModel(int tempPalletsCount, int shipPalletsCount, List<BaseShipView> shipViews)
    {
        _tempPalletsCnt = tempPalletsCount;
        _shipPalletsCnt = shipPalletsCount;
        //_parrotBlockViews = parrotBlockViews;
        _shipViews = shipViews;
    }

    public void SetTempPalletsCount()
    {
        _tempPalletsCnt += 1;
        TempPalletsCntChanged.Execute(_tempPalletsCnt);
        YG2.SaveProgress();
    }

    public void SetShipPalletsCount()
    {
        _shipPalletsCnt+=1;
        TempPalletsCntChanged.Execute(_shipPalletsCnt);
        YG2.SaveProgress();
    }

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
