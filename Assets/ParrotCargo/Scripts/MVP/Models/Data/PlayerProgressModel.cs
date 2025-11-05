using System;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerProgressModel
{
    private int _coins;
    private int _score;

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
        if (_coins != value)
        {
            _coins = value;
            CoinsChanged.Execute(_coins);
        }
    }

    public void SetScore(int value)
    {
        if (_score != value)
        {
            _score = value;
            ScoreChanged.Execute(_score);
        }
    }

    public void AllChanged()
    {
        CoinsChanged.Execute(_coins);
        ScoreChanged.Execute(_score);
    }
}
