using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;
using UniRx;
using YG;

public class ShopService : BaseService
{
    [SerializeField] private ShopView _view;
    [SerializeField] private List<ParrotsBlockView> _parrotViews;
    [SerializeField] private List<ShipSetting> _ship;
    [SerializeField] private ShopSpawner _shopSpawner;

    [Inject] PlayerProgressService _playerProgressService;
    [Inject] PalletService _palletService;
    [Inject] ShipsService _shipService;

    private ShopPresenter _shopPresenter;
    private ShopModel _shopModel;

    public ShopPresenter ShopPresenter => _shopPresenter;

    public override void Initialize()
    {
        _shopModel = YG2.saves.shopModel;
        _shopSpawner.Spawn();

        _shopPresenter = new ShopPresenter(_shopModel, _view, _shopSpawner.ShopItems.ToList());
        _shopPresenter.PurchaseCommand.Subscribe(price => _playerProgressService.DecreaseOnPurchase(price));
        _shopPresenter.SubModelChanged.Subscribe(itemType => OnPresenterChanged(itemType));
        _shopPresenter.Initialize();
    }

    public void OnSave()
    {
        YG2.saves.shopModel = _shopModel;
    }

    private void OnPresenterChanged(TypeShopItem itemType)
    {
        switch (itemType)
        {
            case TypeShopItem.PalletUpgrade:
                _palletService.OnPalletUpgrade();
                break;
            case TypeShopItem.ShipUpgrade:
                _shipService.OnShipUpgrade();
                break;
            case TypeShopItem.ParrotPurchase:
                break;
            case TypeShopItem.ShipPurchase:
                break;
        }
    }
}
