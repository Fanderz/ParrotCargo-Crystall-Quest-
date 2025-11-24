using UnityEngine;
using UniRx;
using static UnityEngine.Rendering.DebugUI;
using YG;

public class CoinsPresenter
{
    private CoinsModel _model;
    private CoinsView _view;

    public CoinsPresenter(CoinsModel model, CoinsView view, float smoothIncreaserWait)
    {
        _model = model;
        _view = view;

        Initialize();
        _view.Initialize(smoothIncreaserWait);
    }

    public void Initialize()
    {
        _model.ValueChanged.Subscribe(value => { _view.ChangeValue(value); });
        //_view.ValueChanged.Subscribe(value => { _model.ChangeValue(value); });

        _model.Initialize();
    }

    public void IncreaseCoins(int increaseValue)
    {
        _model.ChangeValue(_model.Value + increaseValue);
    }

    public void DecreaseCoins(int price)
    {
        _model.ChangeValue(_model.Value - price);
    }
}
