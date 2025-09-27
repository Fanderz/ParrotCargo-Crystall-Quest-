using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class PlayerProgressService : BaseService
{
    [SerializeField] private PlayerProgressView _view;
    //[SerializeField] private int _increaseValue;

    private PlayerProgressPresenter _presenter;

    public override void Initialize()
    {
        PlayerProgressModel model = new PlayerProgressModel(YG2.saves.playerProgress.Coins, YG2.saves.playerProgress.Score);
        _presenter = new PlayerProgressPresenter(model, _view);
    }
}
