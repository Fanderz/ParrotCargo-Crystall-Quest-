using UniRx;
using UnityEngine;
using Zenject;

public class PalletPresenter
{
    private PalletView _view;
    private Pallet _model;
    private BaseCrystallBagView _bagView;

    public bool isEmpty => _view.HaveBag == false;
    public Transform ViewTransform => _view.transform;

    public ReactiveCommand TakedBag = new ReactiveCommand();

    [Inject]
    public PalletPresenter(PalletView view, Pallet model)
    {
        _view = view;
        _model = model;

        Subscribes();
    }

    public bool HaveCourier { get; private set; }

    public void TakeBag(BaseCrystallBagView crystallBagView)
    {
        _view.TakeBag(crystallBagView);
        crystallBagView.ChangePicked(false);
        HaveCourier = false;
        TakedBag.Execute();
    }

    public void SetCourier(bool value)
    {
        HaveCourier = value;
    }

    private void Subscribes()
    {
        _view.EmptyChanged.Subscribe((value => { _model.ChangeEmpty(value); }));
    }
}
