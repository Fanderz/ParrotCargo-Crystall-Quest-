using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsService : BaseService
{
    [SerializeField] private TextMeshPro _coinsText;
    [SerializeField] private int _increaseValue;

    private int _coins;
    private int _tmpCoins;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerCoins") == false)
            PlayerPrefs.SetInt("PlayerCoins", 0);

        _tmpCoins = 0;
    }

    public override void Initialize()
    {
        _coins = PlayerPrefs.GetInt("PlayerCoins", 0);
    }

    private void Start()
    {
        _coinsText.text = _tmpCoins.ToString();
    }
     
    public void IncreaseCoins()
    {
        _tmpCoins += _increaseValue;
    }
}
