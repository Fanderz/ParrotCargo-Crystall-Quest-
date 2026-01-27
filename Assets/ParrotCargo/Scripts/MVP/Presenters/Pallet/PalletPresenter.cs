using UniRx;
using UnityEngine;
using Zenject;

public class PalletPresenter
{
    private PalletView _view;
    private Pallet _model;
    private BaseCrystallBagView _bagView;

    public bool isEmpty => _view.HaveBag == false;
    public bool isActive => _view.gameObject.activeSelf;
    public Transform ViewTransform => _view.transform;

    public ReactiveCommand TakedBag = new ReactiveCommand();

    public PalletPresenter(PalletView view, Pallet model)
    {
        _view = view;
        _model = model;

        if (_view != null && _model != null)
            _view.EmptyChanged.Subscribe(value => _model.ChangeEmpty(value));
    }

    public bool HaveCourier { get; private set; }

    public void TakeBag(BaseCrystallBagView crystallBagView)
    {
        _view.OnTakeBag(crystallBagView);
        crystallBagView.ChangePicked(false);
        HaveCourier = false;
        TakedBag.Execute();
    }

    public void SetCourier(bool value)
    {
        HaveCourier = value;
    }
}

public class NullablePalletPresenter : PalletPresenter
{
    public NullablePalletPresenter(PalletView view, Pallet model) : base(view, model)
    {
    }
}
