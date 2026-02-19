using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using YG;
using UniRx;
using TMPro;
using Zenject;
using Cysharp.Threading.Tasks;
using Assets.ParrotCargo.Scripts.MVP.Models.Data;

public class PlayerProgressService : BaseService
{
    [Header("Coins and Points Setts")]
    [SerializeField] private CoinsView _gameCoinsView;
    [SerializeField] private CoinsView _shopCoinsView;
    [SerializeField] private PointsView _gamePointsView;
    [SerializeField] private int _crystallBagPrice;
    [SerializeField] private int _pointsIncreaseValue;
    [SerializeField] private float _smoothChangeWait;

    [Header("GameOver Setts")]
    [SerializeField] private GameObject _gameOverView;
    [SerializeField] private GameObject _rewardButton;
    [SerializeField] private PointsView _gameOverScoreView;
    [SerializeField] private CoinsView _gameOverCoinsView;
    [SerializeField] private List<PanelAnimationView> _panelsAnimationView;

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
    private CoinsPresenter _gameOverCoinsPresenter;
    private PointsPresenter _gamePointsPresenter;
    private PointsPresenter _gameOverPointsPresenter;
    private PointsPresenter _lederboardPointsPresenter;

    [Inject] private AudioService _audioService;
    [Inject] private SmoothLoaderService _smoothLoaderService;
    [Inject] private AdsService _adsService;

    public override void Initialize()
    {
        OnYGInit();

        _shopCoinsPresenter = new CoinsPresenter(YG2.saves.coinsProgress, _shopCoinsView, _smoothChangeWait);
        _gameCoinsPresenter = new CoinsPresenter(_gameCoinsModel, _gameCoinsView, _smoothChangeWait);
        _gameOverCoinsPresenter = new CoinsPresenter(_gameCoinsModel, _gameOverCoinsView, _smoothChangeWait);

        _gamePointsPresenter = new PointsPresenter(_pointsModel, _gamePointsView, _smoothChangeWait);
        _gameOverPointsPresenter = new PointsPresenter(_pointsModel, _gameOverScoreView, _smoothChangeWait);
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

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public async void OnGameOver()
    {
        _audioService.OnGameLose();
        _gameOverView.SetActive(true);

        if (_gameCoinsModel.Value == 0)
            _rewardButton.SetActive(false);

        foreach (var panelAnimationView in _panelsAnimationView)
            panelAnimationView.Show();

        await UniTask.Delay(1000);

        SetTimeScale(0);
    }

    public void OnReward(string id)
    {
        if (id == _adsService.Id)
        {
            _gameOverCoinsPresenter.IncreaseCoins(_gameCoinsModel.Value);

            YG2.onCloseRewardedAdv += (async () =>
            {
                await UniTask.Delay(1200);
                SetTimeScale(0);
            });
        }
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

    public async void ResetProgress()
    {
        SceneService.Instance.RestartScene();
        SceneService.Instance.SetTimeScale(1f);
    }

    private List<ShopSaveData> CreateDefaultItems(List<BaseShopItemValuesSO> shopItemSettings)
    {
        List<ShopSaveData> save = new List<ShopSaveData>();

        foreach (BaseShopItemValuesSO setting in shopItemSettings)
        {
            for (int j = 1; j <= setting.ItemChildCount; j++)
                save.Add(new ShopSaveData
                {
                    IsPurchased = (j <= setting.DefaulPurchasedCount ? true : false),
                    isActive = (j <= setting.DefaultActiveCount ? true : false),
                    Type = setting.ItemName
                });
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
            YG2.saves.playerSettings = new SettingsModel(1f, 0.05f);

        if (YG2.saves.coinsProgress == null)
            YG2.saves.coinsProgress = new CoinsModel(0);

        if (YG2.saves.pointsProgress == null)
            YG2.saves.pointsProgress = new PointsModel(0);

        if (YG2.saves.pointsProgress.Value == 0)
        {
            YG2.saves.isFirstGame = true;
            YG2.saves.currentTypeBird = TypeBird.Parrot;
            YG2.saves.currentTypeShip = TypeShip.Pirate;
        }

        YG2.SaveProgress();
    }
}
