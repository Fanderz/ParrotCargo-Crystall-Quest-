using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

[Serializable]
public class ShopModel
{
    private int _tempPalletsCnt = 2;
    private int _shipPalletsCnt = 1;

    private List<ParrotsBlockView> _parrotBlockViews;
    private BaseShipView _shipView;

    public int TempPalletsCount => _tempPalletsCnt;
    public int ShipPalletsCount => _shipPalletsCnt;
    public IReadOnlyList<ParrotsBlockView> ParrotBlockViews => _parrotBlockViews;
    public BaseShipView ShipView => _shipView;

    public ReactiveCommand<int> TempPalletsCntChanged = new ReactiveCommand<int>();
    public ReactiveCommand<int> ShipPalletsCntChanged = new ReactiveCommand<int>();
    public ReactiveCommand<List<ParrotsBlockView>> ParrotBlockViewsChanged = new ReactiveCommand<List<ParrotsBlockView>>();
    public ReactiveCommand<BaseShipView> ShipViewsChanged = new ReactiveCommand<BaseShipView>();

    public ShopModel(int tempPalletsCount, int shipPalletsCount, List<ParrotsBlockView> parrotBlockViews, BaseShipView shipView)
    {
        _tempPalletsCnt = tempPalletsCount;
        _shipPalletsCnt = shipPalletsCount;
        _parrotBlockViews = parrotBlockViews;
        _shipView = shipView;
    }

    public void SetTempPalletsCount(int value)
    {
        _tempPalletsCnt = value;
        TempPalletsCntChanged.Execute(_tempPalletsCnt);
    }

    public void SetShipPalletsCount(int value)
    {
        _shipPalletsCnt = value;
        TempPalletsCntChanged.Execute(_shipPalletsCnt);
    }

    public void SetParrotsViews(List<ParrotsBlockView> parrotBlockViews)
    {
        _parrotBlockViews = parrotBlockViews.ToList();
        ParrotBlockViewsChanged.Execute(_parrotBlockViews);
    }

    public void SetShipView(BaseShipView shipView)
    {
        _shipView = shipView;
        ShipViewsChanged.Execute(_shipView);
    }
}
