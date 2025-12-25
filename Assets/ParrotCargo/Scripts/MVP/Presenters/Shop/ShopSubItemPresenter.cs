using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSubItemPresenter
{
    private ShopSubItemView _view;
    private ShopSaveData _model;

    public ShopSubItemPresenter(ShopSubItemView view, ShopSaveData model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        SetPurchasedOnLoad();
    }

    private void SetPurchasedOnLoad()
    {
        if (_model.IsPurchased)
            _view.OnPurchase();
    }
}
