using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using YG;

public class ShopPresenter
{
    private ShopModel _model;
    private ShopView _view;
    private CoinsModel _wallet;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();
    public ShopModel ShopModel => _model;

    public ShopPresenter(ShopModel model, ShopView view, List<ShopItem> shopItems)
    {
        _model = model;
        _view = view;
        _view.Initialize(shopItems);
        _wallet = YG2.saves.coinsProgress;
    }

    public void Initialize()
    {
        //_view.SetStarsFilledOnLoad();
        //_view.SetShipStarsFilled(_model.ShipPalletsCount);
        //_view.SetPalletStarsFilled(_model.TempPalletsCount);
        //_view.SetStarsFilledOnLoad(_model.ShipPalletsCount, "");
        //_view.SetStarsFilledOnLoad(_model.TempPalletsCount, "");

        //_view.ShipStarFilledCommand.Subscribe( cnt => _model.SetShipPalletsCount() );
        //_view.PalletStarFilledCommand.Subscribe( cnt => _model.SetTempPalletsCount() );
        //_view.StarFilledCommand.Subscribe(() => _model.Set)
    }

    private void TryPurchase(int price)
    {
        if(CanPurchase(price))
            PurchaseCommand?.Execute(price);
    }

    private bool CanPurchase(int price)
    {
        return price <= _wallet.Value;
    }
}
