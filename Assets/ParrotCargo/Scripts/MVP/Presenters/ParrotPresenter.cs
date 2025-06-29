using System.Collections;
using UniRx;

public class ParrotPresenter
{
    private ParrotView _view;
    private Parrot _model;

    public ParrotPresenter(ParrotView view, Parrot model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        _view.PickedBag.Subscribe(crystallBag => { _model.PickBag(crystallBag); });
    }
}
