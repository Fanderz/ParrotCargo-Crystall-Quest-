using System;
using UniRx;
using UnityEngine;

//[Serializable]
public class CoinsModel : BaseScoreModel
{
    public CoinsModel(int inputValue)
    {
        value = inputValue;
    }
    //private int _coins;

    //public int Coins => _coins;

    //public ReactiveCommand<int> CoinsChanged = new ReactiveCommand<int>();

    //public CoinsModel(int coins, int score)
    //{
    //    _coins = coins;
    //}

    //public void SetCoins(int value)
    //{
    //    if (_coins != value)
    //    {
    //        _coins = value;
    //        CoinsChanged.Execute(_coins);
    //    }
    //}

    //public void AllChanged()
    //{
    //    CoinsChanged.Execute(_coins);
    //}
}
