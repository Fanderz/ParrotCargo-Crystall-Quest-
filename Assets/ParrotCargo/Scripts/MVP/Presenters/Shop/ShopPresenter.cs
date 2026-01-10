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

    public ShopPresenter(ShopModel model, ShopView view, List<ShopItemPresenter> shopItems)
    {
        _model = model;
        _view = view;
        _shopItemPresenters = shopItems;
        _wallet = YG2.saves.coinsProgress;
    }

    public void Initialize()
    {
        foreach (var presenter in _shopItemPresenters)
        {
            presenter.Purchasing.Subscribe(subItem => TryPurchase(subItem, presenter.ItemType));
            _model.ModelChanged.Subscribe(data => presenter.OnModelChanged(data));
        }
    }

    private void TryPurchase(ShopSubItemPresenter subItemPresenter, TypeShopItem type)
    {
        bool success;

        if (subItemPresenter.IsActivated)
            return;

        if (subItemPresenter.IsPurchaseItem && subItemPresenter.IsPurchased)
        {
            _model.ActivatePurchase(subItemPresenter.SaveData);
            return;
        }

        if (CanPurchase(subItemPresenter.Price) == false)
            return;

        success = _model.Purchase(subItemPresenter.SaveData);


        if (success == false)
            return;

        PurchaseCommand.Execute(subItemPresenter.Price);
    }

    private bool CanPurchase(int price)
    {
        return _wallet.Value != 0 ? price <= _wallet.Value : false;
    }
}
