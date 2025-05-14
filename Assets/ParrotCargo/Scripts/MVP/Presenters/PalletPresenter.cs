public class PalletPresenter
{
    private PalletView _view;
    private Pallet _palletModel;

    public PalletPresenter(PalletView view, Pallet model)
    {
        _view = view;
        _palletModel = model;
    }
}
