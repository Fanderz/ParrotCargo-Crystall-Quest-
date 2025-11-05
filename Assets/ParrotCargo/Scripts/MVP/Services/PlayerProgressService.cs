using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class PlayerProgressService : BaseService
{
    [SerializeField] private PlayerProgressView _view;
    //[SerializeField] private int _increaseCoinsValue;
    //[SerializeField] private int _increaseScoreValue;

    private PlayerProgressPresenter _presenter;

    public override void Initialize()
    {
        _presenter = new PlayerProgressPresenter(YG2.saves.playerProgress, _view);
    }
}
