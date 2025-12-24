using UniRx;

public class ShopItemPresenter
{
    private readonly UpgradesShopItemView _view;
    private readonly TypeShopItem _type;

    public ReactiveCommand<ShopSubItemView> TryPurchase = new ReactiveCommand<ShopSubItemView>();

    public ShopItemPresenter(UpgradesShopItemView view)
    {
        _view = view;
        _type = view.ItemType;
    }

    public TypeShopItem ItemType => _type;

    public void Initialize(int purchasedCount)
    {
        _view.SetStarsFilledOnLoad(purchasedCount);

        _view.TryPurchase.Subscribe(subItem => TryPurchase.Execute(subItem));
    }

    public void OnModelChanged(int newPurchasedCount)
    {
        _view.SetStarsFilledOnLoad(newPurchasedCount);
    }


}
