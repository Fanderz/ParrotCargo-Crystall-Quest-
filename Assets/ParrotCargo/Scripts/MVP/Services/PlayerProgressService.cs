using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        if (_pointsModel.Value > YG2.saves.pointsProgress.Value)
            YG2.SetLeaderboard("BestPlayers", _pointsModel.Value);

        _settingService.OnSave();

        YG2.SaveProgress();
    }

    public void ResetProgress()
    {
        SceneManager.LoadScene("GameScene");
    }

    private List<ShopSaveData> CreateDefaultItems(List<BaseShopItemValuesSO> shopItemSettings)
    {
        List<ShopSaveData> save = new List<ShopSaveData>();

        foreach (BaseShopItemValuesSO setting in shopItemSettings)
        {
            for (int j = 1; j <= setting.ItemChildCount; j++)
                save.Add(new ShopSaveData { IsPurchased = (j <= setting.DefaulPurchasedCount ? true : false), isActive = (j <= setting.DefaultActiveCount ? true : false), Type = setting.ItemName });
        }

        return save;
    }

    private void OnYGInit()
    {
        _gameCoinsModel = new CoinsModel(0);
        _pointsModel = new PointsModel(0);

        if (YG2.saves.shopModel == null)
            YG2.saves.shopModel = new ShopSaveModel
            {
                upgradeItems = CreateDefaultItems(_shopService.UpgradeItemsSettings.ToList()),
                purchaseItems = CreateDefaultItems(_shopService.PurchaseItemsSettings.ToList())
            };

        if (YG2.saves.playerSettings == null)
            YG2.saves.playerSettings = new SettingsModel(1f, 1f);

        if (YG2.saves.coinsProgress == null)
            YG2.saves.coinsProgress = new CoinsModel(0);

        if (YG2.saves.pointsProgress == null)
            YG2.saves.pointsProgress = new PointsModel(0);

        YG2.SaveProgress();
    }
}
