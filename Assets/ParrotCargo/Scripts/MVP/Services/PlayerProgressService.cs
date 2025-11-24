using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using YG.Insides;

public class PlayerProgressService : BaseService
{
    [SerializeField] private CoinsView _gameCoinsView;
    [SerializeField] private CoinsView _shopCoinsView;
    [SerializeField] private PointsView _pointsView;
    [SerializeField] private int _crystallBagPrice;
    [SerializeField] private int _pointsIncreaseValue;
    [SerializeField] private float _smoothChangeWait;

    private CoinsModel _gameCoinsModel;
    private CoinsPresenter _shopCoinsPresenter;
    private CoinsPresenter _gameCoinsPresenter;
    private PointsPresenter _pointsPresenter;

    public override void Initialize()
    {
        _gameCoinsModel = new CoinsModel(0);

        _shopCoinsPresenter = new CoinsPresenter(YG2.saves.coinsProgress,  _shopCoinsView, _smoothChangeWait);
        _gameCoinsPresenter = new CoinsPresenter(_gameCoinsModel, _gameCoinsView, _smoothChangeWait);

        _pointsPresenter = new PointsPresenter(YG2.saves.pointsProgress, _pointsView, _smoothChangeWait);
    }

    public void IncreaseValuesOnBagRelease()
    {
        _gameCoinsPresenter.IncreaseCoins(_crystallBagPrice);
        _shopCoinsPresenter.IncreaseCoins(_crystallBagPrice);

        _pointsPresenter.IncreaseScore(_pointsIncreaseValue);
    }

    public void SaveProgress()
    {
        YG2.saves.coinsProgress.Value += _gameCoinsModel.Value;
        YG2.SaveProgress();

    }
}
