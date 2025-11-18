using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class PlayerProgressService : BaseService
{
    [SerializeField] private CoinsView _coinsView;
    [SerializeField] private PointsView _pointsView;
    [SerializeField] private int _crystallBagPrice;
    [SerializeField] private int _pointsIncreaseValue;
    [SerializeField] private float _smoothIncreaseWait;

    private CoinsPresenter _coinsPresenter;
    private PointsPresenter _pointsPresenter;

    public override void Initialize()
    {
        _coinsPresenter = new CoinsPresenter(YG2.saves.coinsProgress, _coinsView, _smoothIncreaseWait);
        _pointsPresenter = new PointsPresenter(YG2.saves.pointsProgress, _pointsView, _smoothIncreaseWait);
    }

    public void IncreaseValuesOnBagRelease()
    {
        _coinsPresenter.IncreaseCoins(_crystallBagPrice);
        _pointsPresenter.IncreaseScore(_pointsIncreaseValue);
    }
}
