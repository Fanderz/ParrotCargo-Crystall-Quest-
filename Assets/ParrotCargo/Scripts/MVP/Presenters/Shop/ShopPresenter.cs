using System.Collections.Generic;
using System.Linq;
using UniRx;
using YG;

public class ShopPresenter
{
    private ShopModel _model;
    private ShopView _view;
    private CoinsModel _wallet;

    private List<ShopItemPresenter> _shopItemPresenters;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();
    public ReactiveCommand<TypeShopItem> SubModelChanged = new ReactiveCommand<TypeShopItem>();

    public ShopPresenter(ShopModel model, ShopView view, List<ShopItemPresenter> shopItems)
    {

        _model = model;
        _view = view;
        _shopItemPresenters = shopItems;
        _wallet = YG2.saves.coinsProgress;

        //_view.Initialize(shopItems);
    }

    public void Initialize()
    {
        foreach (var presenter in _shopItemPresenters)
            presenter.TryPurchase.Subscribe(subItem => TryPurchase(subItem, presenter.ItemType));

        _model.UpgradeItemChanged.Subscribe(type =>
            {
                foreach (var presenter in _shopItemPresenters)
                {
                    if (presenter.ItemType != type)
                        continue;

                    int cnt = type == TypeShopItem.PalletUpgrade ? _model.TempPalletsCnt : _model.ShipPalletsCnt;

                    presenter.OnModelChanged(cnt);
                }
            });
    }

    private void TryPurchase(ShopSubItemView subItem, TypeShopItem type)
    {
        if (CanPurchase(subItem.Price) == false)
            return;

        if (_model.CanPurchaseUpgrade(type) == false)
            return;

        bool success = _model.PurchaseUpgrade(type);

        if (!success)
            return;

        PurchaseCommand.Execute(subItem.Price);
        subItem.OnPurchase();
    }

    private bool CanPurchase(int price)
    {
        return _wallet.Value != 0 ? price <= _wallet.Value : false;
    }
}
