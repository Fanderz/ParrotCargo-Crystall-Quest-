using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class PlayerProgressView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsView;
    [SerializeField] private TextMeshProUGUI _scoreView;

    public ReactiveCommand<int> CoinsChanged = new ReactiveCommand<int>();
    public ReactiveCommand<int> ScoreChanged = new ReactiveCommand<int>();

    public void SetCoins(int value)
    {
        _coinsView.text = value.ToString();
        CoinsChanged.Execute(value);
    }

    public void SetScore(int value)
    {
        _scoreView.text = value.ToString();
        ScoreChanged.Execute(value);
    }
}
