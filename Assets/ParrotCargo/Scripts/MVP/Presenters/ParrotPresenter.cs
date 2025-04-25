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

    public void Initialize()
    {
        _view.MoveCommand.Subscribe(position => { _parrotModel.SetPosition(position); });
        _parrotModel.UpdatedPositionCommand.Subscribe(positionModel => { _view.UpdatePosition(positionModel); });
    }
}
