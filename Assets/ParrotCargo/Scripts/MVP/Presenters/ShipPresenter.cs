public class ShipPresenter
{
    private BaseShipView _shipView;
    private Ship _shipModel;

    public ShipPresenter(BaseShipView view, Ship model)
    {
        _shipView = view;
        _shipModel = model;
    }
}
