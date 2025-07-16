using Zenject;

public class ShipPresenter
{
    [Inject]
    public ShipPresenter(BaseShipView view, Ship model)
    {
        ShipView = view;
        ShipModel = model;
    }

    public BaseShipView ShipView { get; private set; }
    public Ship ShipModel { get; private set; }
}
