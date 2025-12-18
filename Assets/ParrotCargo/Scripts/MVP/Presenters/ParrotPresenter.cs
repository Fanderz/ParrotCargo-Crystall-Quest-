using UniRx;

public class ParrotPresenter
{
    private ParrotView _view;
    private Parrot _model;

    private PalletPresenter _tempPallet;

    public BaseCrystallBagView CrystallBag => _view.CrystallBag;
    //public bool HaveBag => _view.HaveBag;
    public bool isActive => _view.gameObject.activeSelf;

    public ReactiveCommand PickedBag = new ReactiveCommand();
    public ReactiveCommand DroppedBag = new ReactiveCommand();
    public ReactiveCommand ChangedActive = new ReactiveCommand();
    public ReactiveCommand SittingWithBag = new ReactiveCommand();

    public ParrotPresenter(ParrotView view, Parrot model)
    {
        _view = view;
        _model = model;
    }

    public PalletPresenter TargetPallet { get; private set; }

    public void Initialize()
    {
        _view.PickedBag.Subscribe(crystallBag => { _model.PickBag(crystallBag); });
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

        if (isTargetShip == false)
            _tempPallet = targetPallet;

        if (isTargetShip && _tempPallet != null)
            _tempPallet.SetCourier(false);

        _view.CarryBag(TargetPallet.ViewTransform, isTargetShip);
    }

    public void ReturnParrotOnStart() =>
        _view.ReturnToStartPoint();

    public void OnBlockMoving(bool isMoving) =>
        _view.SetParrotMovable(isMoving);

    public ParrotView GetView() => _view;
}
