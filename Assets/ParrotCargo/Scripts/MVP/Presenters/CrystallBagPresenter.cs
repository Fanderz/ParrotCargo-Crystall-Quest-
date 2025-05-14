public class CrystallBagPresenter
{
    private BaseCrystallBagView _view;
    private BaseCrystallBag _crystallBagModel;

    public CrystallBagPresenter(BaseCrystallBagView view, BaseCrystallBag model)
    {
        _view = view;
        _crystallBagModel = model;
    }
}
