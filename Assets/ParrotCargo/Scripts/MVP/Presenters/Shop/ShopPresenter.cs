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
    public ReactiveCommand ActivatedItem = new ReactiveCommand();
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
            _model.UpgradeChanged.Subscribe(data => presenter.OnModelChanged(data));
            _model.SkinChanged.Subscribe(OnActivateSubItem);
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
            _shopItemPresenters.First(presenter => presenter.ItemType == type).SubItemPresenters.ToList().ForEach(subItemPresenter => subItemPresenter.SetUnActive());
            subItemPresenter.SetActive();
            ActivatedItem.Execute();
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

    private void OnActivateSubItem((int index, TypeShopItem type) input)
    {
        _shopItemPresenters.Find(presenter => presenter.ItemType == input.type).SubItemPresenters.ToList().
            ForEach(subItemPresenter => subItemPresenter.SetUnActive());

        _shopItemPresenters.Find(presenter => presenter.ItemType == input.type).SubItemPresenters[input.index].SetActive();
    }
}
