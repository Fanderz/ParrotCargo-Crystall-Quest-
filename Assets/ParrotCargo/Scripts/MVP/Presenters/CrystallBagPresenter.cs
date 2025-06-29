using UniRx;

public class CrystallBagPresenter
{
    private BaseCrystallBagView _view;
    private BaseCrystallBag _model;

    public CrystallBagPresenter(BaseCrystallBagView view, BaseCrystallBag model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        _view.Picked.Subscribe(picked => { _model.SetPicked(picked); });
    }
}
