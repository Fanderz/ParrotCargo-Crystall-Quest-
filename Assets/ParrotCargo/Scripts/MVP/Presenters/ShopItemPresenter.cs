using UniRx;

public class ShopItemPresenter
{
    private ShopItem _view;
    private ShopItemModel _model;

    public ReactiveCommand<ShopSubItem> TryPurchase = new ReactiveCommand<ShopSubItem>();
    public ReactiveCommand<TypeShopItem> ModelChangedCommand = new ReactiveCommand<TypeShopItem>();

    public ShopItemPresenter(ShopItem view, ShopItemModel model)
    {
        _view = view;
        _model = model;
    }

    public void Initialize()
    {
        if (_view is UpgradesShopItem && _model is UpgradeShopItemModel)
        {
            UpgradesShopItem upgradeItem = (UpgradesShopItem)_view;
            UpgradeShopItemModel upgradeItemModel = (UpgradeShopItemModel)_model;

            upgradeItem.SetStarsFilledOnLoad(upgradeItemModel.ObjectsCnt);
            upgradeItem.TryPurchase.Subscribe(subItem => TryPurchase.Execute(subItem));
            upgradeItemModel.ObjectsCntChanged.Subscribe(exec =>
            {
                ModelChangedCommand.Execute(upgradeItemModel.ItemType);
            });

            foreach (UpgradeShopSubItem subItem in upgradeItem.SubItems)
                subItem.StarFilledCommand.Subscribe(cmd => upgradeItemModel.AddObject());
        }
    }
}
