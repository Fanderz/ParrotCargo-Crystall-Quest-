using UniRx;
using UnityEngine;

public class CrystallBagPresenter
{
    private BaseCrystallBagView _view;
    private BaseCrystallBag _model;

    //public BaseCrystallBagView CrystallBag => _view;

    public ReactiveCommand<Vector3> BagPicked = new ReactiveCommand<Vector3>();

    public CrystallBagPresenter(BaseCrystallBagView view, BaseCrystallBag model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        _view.Picked.Subscribe(picked => { _model.SetPicked(picked); BagPicked.Execute(_model.StartPosition); });
    }

    public BaseCrystallBagView GetView()
    {
        return _view;
    }
}
