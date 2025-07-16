using UniRx;
using Zenject;

public class PalletPresenter
{
    [Inject]
    public PalletPresenter(PalletView view, Pallet model)
    {
        PalletView = view;
        PalletModel = model;

        Subscribes();
    }

    public PalletView PalletView { get; private set; }
    public Pallet PalletModel { get; private set; }

    private void Subscribes()
    {
        PalletView.EmptyChanged.Subscribe((value => { PalletModel.ChangeEmpty(value); }));
    }
}
