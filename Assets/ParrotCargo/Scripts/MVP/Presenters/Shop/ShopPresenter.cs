using System.Collections.Generic;

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

    public ShopPresenter(ShopModel model, ShopView view, List<ShopItemView> shopItems)
    {
        _shopItemPresenters = new List<ShopItemPresenter>();

        _model = model;
        _view = view;
        _wallet = YG2.saves.coinsProgress;

        _view.Initialize(shopItems);

    }

    public void Initialize()
    {
        foreach (var itemView in _view.ShopItems)
        {
            if (itemView is not UpgradesShopItemView upgradesView)
                continue;

            var presenter = new ShopItemPresenter(upgradesView);

            int initialCnt = upgradesView.ItemType switch
            {
                TypeShopItem.PalletUpgrade => _model.TempPalletsCnt,
                TypeShopItem.ShipUpgrade => _model.ShipPalletsCnt,
                _ => 1
            };

            presenter.Initialize(initialCnt);

            presenter.TryPurchase.Subscribe(subItem => TryPurchase(subItem, presenter.ItemType));

            _shopItemPresenters.Add(presenter);
        }

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
