using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPresenter
{
    private ShopModel _model;
    private ShopView _view;

    public ShopPresenter(ShopModel model, ShopView view)
    {
        _model = model;
        _view = view;

        Initialize();
    }

    private void Initialize()
    {
        _view.SetShipStarsFilled(_model.ShipPalletsCount);
        _view.SetPalletStarsFilled(_model.TempPalletsCount);

        //_view.StarFilledCommand.Subscribe(_model.)
        //_model.TempPalletsCntChanged.Subscribe(cnt => _view.)
        //_model.ShipPalletsCntChanged.Subscribe(cnt => _view.)
        //_model.ParrotBlockViewsChanged.Subscribe(views => _view.)
        //_model.ShipViewsChanged.Subscribe(view => _view.)
    }
}
