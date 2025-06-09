using System.Collections;
using UniRx;

public class ParrotPresenter
{
    private ParrotView _view;
    private Parrot _parrotModel;

    public ParrotPresenter(ParrotView view, Parrot model)
    {
        _view = view;
        _parrotModel = model;
    }

    public bool CanParrotPick { get { return _parrotModel.CanPick; } }

    public void Initialize()
    {
        _view.PickingBag.Subscribe(canPick => { _parrotModel.SetPickable(canPick); });
    }

    //public void SearchBags()
    //{
    //    _view.SearchBag();
    //}
}
