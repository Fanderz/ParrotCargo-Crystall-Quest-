using UnityEngine;
using UniRx;

public class PlayerProgressPresenter
{
    private PlayerProgressModel _model;
    private PlayerProgressView _view;

    public PlayerProgressPresenter(PlayerProgressModel model, PlayerProgressView view)
    {
        _model = model;
        _view = view;

        Initialize();
    }

    public void Initialize()
    {
        _model.CoinsChanged.Subscribe(value => { _view.SetCoins(value); });
        _model.ScoreChanged.Subscribe(value => { _view.SetScore(value); });

        _view.CoinsChanged.Subscribe(value => { _model.SetCoins(value); });
        _view.ScoreChanged.Subscribe(value => { _model.SetScore(value); });

        _model.AllChanged();
    }

    public void IncreaseCoins(int increaseValue)
    {
        _model.SetCoins(_model.Coins + increaseValue);
    }

    public void DecreaseCoins(int price)
    {
        _model.SetCoins(_model.Coins - price);
    }

    public void IncreaseScore(int increaseValue)
    {
        _model.SetScore(_model.Score + increaseValue);
    }

}
