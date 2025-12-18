using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Assets.ParrotCargo.Scripts.MVP.Models.Data;

using YG;

public class PlayerProgressService : BaseService
{
    [Header("Coins and Points Setts")]
    [SerializeField] private CoinsView _gameCoinsView;
    [SerializeField] private CoinsView _shopCoinsView;
    [SerializeField] private PointsView _gamePointsView;
    [SerializeField] private int _crystallBagPrice;
    [SerializeField] private int _pointsIncreaseValue;
    [SerializeField] private float _smoothChangeWait;

    [Header("Sounds Setts")]
    [SerializeField] private SettingsService _settingService;

    [Header("Shop Setts")]
    [SerializeField] private ShopService _shopService;

    [Header("Pallet Setts")]
    [SerializeField] private PalletService _palletService;

    private CoinsModel _gameCoinsModel;
    private PointsModel _pointsModel;

    private CoinsPresenter _shopCoinsPresenter;
    private CoinsPresenter _gameCoinsPresenter;
    private PointsPresenter _gamePointsPresenter;
    private PointsPresenter _lederboardPointsPresenter;

    public override void Initialize()
    {
        OnYGInit();

        _shopCoinsPresenter = new CoinsPresenter(YG2.saves.coinsProgress, _shopCoinsView, _smoothChangeWait);
        _gameCoinsPresenter = new CoinsPresenter(_gameCoinsModel, _gameCoinsView, _smoothChangeWait);

        _gamePointsPresenter = new PointsPresenter(_pointsModel, _gamePointsView, _smoothChangeWait);
        //_lederboardPointsPresenter = new PointsPresenter(YG2.saves.pointsProgress, _leaderbordPointsView, _smoothChangeWait);
    }

    public void IncreaseValuesOnBagRelease()
    {
        _gameCoinsPresenter.IncreaseCoins(_crystallBagPrice);
        _gamePointsPresenter.IncreaseScore(_pointsIncreaseValue);
    }

    public void DecreaseOnPurchase(int price)
    {
        _shopCoinsPresenter.DecreaseCoins(price);
        SaveProgress();
    }

    public void SaveProgress()
    {
        YG2.saves.coinsProgress.Value += _gameCoinsModel.Value;
        YG2.saves.pointsProgress.Value += _pointsModel.Value;
        _settingService.OnSave();
        _shopService.OnSave();
        _palletService.OnSave();

        YG2.SaveProgress();
    }

    public void ResetProgress()
    {
        SceneManager.LoadScene("GameScene");
    }

    private ShopModel CreateDefault()
    {
        return new ShopModel(
            new List<UpgradeShopItemModel>
            {
                new UpgradeShopItemModel(2, TypeShopItem.PalletUpgrade),
                new UpgradeShopItemModel(1, TypeShopItem.ShipUpgrade)
            },
            new List<BaseShipView>());
    }

    private void OnYGInit()
    {
        _gameCoinsModel = new CoinsModel(0);
        _pointsModel = new PointsModel(0);

        if (YG2.saves.shopModel == null)
        {
            YG2.saves.shopModel = CreateDefault();
            YG2.SaveProgress();
        }

        if (YG2.saves.shopModel == null)
        {
            YG2.saves.playerSettings = new SettingsModel(1f, 1f);
            YG2.SaveProgress();
        }
    }
}
