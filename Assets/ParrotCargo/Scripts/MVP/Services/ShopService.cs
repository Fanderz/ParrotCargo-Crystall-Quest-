using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class ShopService : BaseService
{
    [SerializeField] private ShopView _view;
    [SerializeField] private List<ParrotsBlockView> _parrotViews;
    [SerializeField] private List<ShipSetting> _ship;
    [SerializeField] private ShopSpawner _shopSpawner;

    private ShopPresenter _shopPresenter;

    public ShopPresenter ShopPresenter => _shopPresenter;

    public override void Initialize()
    {
        _shopSpawner.Spawn();

        _shopPresenter = new ShopPresenter(YG2.saves.shopModel, _view, _shopSpawner.ShopItems.ToList());
        _shopPresenter.Initialize();
    }
}
