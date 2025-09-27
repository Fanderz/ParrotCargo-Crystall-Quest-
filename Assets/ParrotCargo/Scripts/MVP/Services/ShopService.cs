using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class ShopService : BaseService
{
    [SerializeField] private ShopView _view;

    private ShopPresenter _shopPresenter;

    public override void Initialize()
    {
        ShopModel model = new ShopModel(YG2.saves.shopModel.TempPalletsCount, YG2.saves.shopModel.ShipPalletsCount, YG2.saves.shopModel.ParrotBlockViews.ToList(), YG2.saves.shopModel.ShipView);
        _shopPresenter = new ShopPresenter(model, _view);
    }
}
