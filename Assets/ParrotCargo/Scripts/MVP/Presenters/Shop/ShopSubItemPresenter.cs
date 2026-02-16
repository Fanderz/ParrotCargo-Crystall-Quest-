using System.Drawing;
using UniRx;
using UnityEngine;

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
        SetActivatedOnLoad();

        _view.Button.onClick.AddListener(() => PurchaseClicked.Execute(this));
    }

    public void SetPurchased()
    {
        _view.OnPurchase();
    }

    public void SetActive()
    {
        var item = _view.GetComponent<PurchaseShopSubItemView>();
        item.SetActiveView();
    }

    public void SetUnActive()
    {
        var item = _view.GetComponent<PurchaseShopSubItemView>();
        item.SetUnActiveView();
    }

    private void SetPurchasedOnLoad()
    {
        if (_model.IsPurchased)
        {
            _view.OnPurchase();
            _view.Button.onClick.RemoveListener(() => PurchaseClicked.Execute(this));
        }
    }

    private void SetActivatedOnLoad()
    {
        if (_model.IsPurchased && _model.isActive)
            SetActive();
    }
}