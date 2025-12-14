using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using YG.Insides;

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
        _gameCoinsModel = new CoinsModel(0);
        _pointsModel = new PointsModel(0);

        _shopCoinsPresenter = new CoinsPresenter(YG2.saves.coinsProgress,  _shopCoinsView, _smoothChangeWait);
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
}
