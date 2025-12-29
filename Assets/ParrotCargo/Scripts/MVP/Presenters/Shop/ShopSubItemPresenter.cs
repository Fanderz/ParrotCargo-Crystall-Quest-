using UniRx;

public class ShopSubItemPresenter
{
    private ShopSubItemView _view;
    private ShopSaveData _model;

    public ReactiveCommand<ShopSubItemPresenter> PurchaseClicked = new ReactiveCommand<ShopSubItemPresenter>();

    public ShopSaveData SaveData => _model;
    public bool IsPurchaseItem => _model.Type == TypeShopItem.ShipPurchase || _model.Type == TypeShopItem.ParrotPurchase;
    public int Price => _view.Price;
    public bool IsPurchased => _model.IsPurchased;
    public bool IsActivated => _model.isActive;

    public ShopSubItemPresenter(ShopSubItemView view, ShopSaveData model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        SetPurchasedOnLoad();

        _view.Button.onClick.AddListener(() =>
        PurchaseClicked.Execute(this));
    }

    private void SetPurchasedOnLoad()
    {
        if (_model.IsPurchased)
            _view.OnPurchase();
    }

    public void SetPurchased()
    {
        _view.OnPurchase();
    }
}
