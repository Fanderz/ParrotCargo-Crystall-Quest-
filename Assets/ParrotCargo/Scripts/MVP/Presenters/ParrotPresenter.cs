using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

public class ParrotPresenter //: IInitializable
{
    private ParrotView _view;
    private Parrot _model;

    public BaseCrystallBagView CrystallBag => _view.CrystallBag;
    public bool HaveBag => _view.HaveBag;
    public bool isActive => _view.gameObject.activeSelf;

    public ReactiveCommand PickedBag = new ReactiveCommand();
    public ReactiveCommand DroppedBag = new ReactiveCommand();
    public ReactiveCommand ChangedActive = new ReactiveCommand();

    public ParrotPresenter(ParrotView view, Parrot model)
    {
        _view = view;
        _model = model;
    }

    public PalletPresenter TargetPallet { get; private set; }

    public void Initialize()
    {
        _view.PickedBag.Subscribe(crystallBag => { _model.PickBag(crystallBag); PickedBag.Execute(); });
        _view.DroppedBag.Subscribe(crystallBag => { _model.PutBag(); DroppedBag.Execute(); });
        _view.ChangedActive.Subscribe(parrot => { ChangedActive.Execute(); });
    }


    public bool IsBagExistsShip(BaseShipView shipView)
    {
        if (shipView is BlueShipView && _view.CrystallBag is BlueCrytallBagView)
            return true;
        else if (shipView is GoldShipView && _view.CrystallBag is GoldCrystallBagView)
            return true;
        else if (shipView is GreenShipView && _view.CrystallBag is GreenCrytallBagView)
            return true;
        else if (shipView is PurpleShipView && _view.CrystallBag is PurpleCrystallBagView)
            return true;
        else
            return false;
    }

    public void CarryBag(PalletPresenter targetPallet, bool isTargetShip)
    {
        TargetPallet = targetPallet;
        _view.CarryBag(TargetPallet.ViewTransform, isTargetShip);
    }

    public void SetActive(bool value)
    {
        _view.SetActive(value);
    }

    public void ReturnParrotOnStart() =>
        _view.ReturnToStartPoint();

    public void OnBlockMoving(bool isMoving) =>
        _view.SetParrotMovable(isMoving);
}
