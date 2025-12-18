using UniRx;

public class PointsPresenter
{
    private PointsModel _model;
    private PointsView _view;

    public PointsPresenter(PointsModel model, PointsView view, float smoothIncreaserWait)
    {
        _model = model;
        _view = view;

        Initialize();
        _view.Initialize(smoothIncreaserWait);
    }

    public void Initialize()
    {
        _model.ValueChanged.Subscribe(value => { _view.ChangeValue(value); });

        _model.Initialize();
    }

    public void IncreaseScore(int increaseValue)
    {
        _model.ChangeValue(_model.Value + increaseValue);
    }
}
