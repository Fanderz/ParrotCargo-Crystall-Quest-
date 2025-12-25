using System.Collections.Generic;

using UniRx;

public class UpgradesShopItemView : ShopItemView
{
    private List<UpgradeShopSubItemView> _subItems;

    public IReadOnlyList<UpgradeShopSubItemView> SubItems => _subItems;

    public ReactiveCommand<UpgradeShopSubItemView> TryPurchase = new ReactiveCommand<UpgradeShopSubItemView>();

    public override void Initialize(ShopItemValues values)
    {
        base.Initialize(values);
        _subItems = new List<UpgradeShopSubItemView>();
    }

    //public override void SetPurchasedOnLoad(ShopSubItemView view)
    //{
    //    _subItems.Find(subItem => subItem == (UpgradeShopSubItemView)view).SetStarFilled();
    //}  

    public UpgradeShopSubItemView CreateSubItem(ShopSubItemView prefab, int price)
    {
        UpgradeShopSubItemView subItem = Instantiate((UpgradeShopSubItemView)prefab, grid.transform);
        subItem.Initialize(price);
        subItem.TryPurchase.Subscribe(clicked => TryPurchase.Execute((UpgradeShopSubItemView)clicked));

        _subItems.Add(subItem);

        return subItem;
    }
}
