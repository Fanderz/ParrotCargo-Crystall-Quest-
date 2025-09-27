using UniRx;
using UnityEngine;

public class PlayerProgressModel
{
    private int _coins = 0;
    private int _score = 0;

    public int Coins => _coins;
    public int Score => _score;

    public ReactiveCommand<int> CoinsChanged = new ReactiveCommand<int>();
    public ReactiveCommand<int> ScoreChanged = new ReactiveCommand<int>();

    public PlayerProgressModel(int coins, int score)
    {
        _coins = coins;
        _score = score;
    }

    public void SetCoins(int value)
    {
        _coins = value;
        CoinsChanged.Execute(_coins);
    }

    public void SetScore(int value)
    {
        _score = value;
        ScoreChanged.Execute(_score);
    }

    public void AllChanged()
    {
        CoinsChanged.Execute(_coins);
        ScoreChanged.Execute(_score);
    }
}
