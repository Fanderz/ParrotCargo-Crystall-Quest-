using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShopPresenter
{
    private ShopModel _model;
    private ShopView _view;

    public ShopModel ShopModel => _model;

    public ShopPresenter(ShopModel model, ShopView view, List<ShopItem> shopItems)
    {
        _model = model;
        _view = view;
        _view.Initialize(shopItems);
    }

    public void Initialize()
    {
        _view.SetShipStarsFilled(_model.ShipPalletsCount);
        _view.SetPalletStarsFilled(_model.TempPalletsCount);

        _view.ShipStarFilledCommand.Subscribe( cnt => _model.SetShipPalletsCount() );
        _view.PalletStarFilledCommand.Subscribe( cnt => _model.SetTempPalletsCount() );

        //_view.StarFilledCommand.Subscribe(_model.)
        //_model.TempPalletsCntChanged.Subscribe(cnt => _view.)
        //_model.ShipPalletsCntChanged.Subscribe(cnt => _view.);
        //_model.ParrotBlockViewsChanged.Subscribe(views => _view.)
        //_model.ShipViewsChanged.Subscribe(view => _view.)
    }
}
