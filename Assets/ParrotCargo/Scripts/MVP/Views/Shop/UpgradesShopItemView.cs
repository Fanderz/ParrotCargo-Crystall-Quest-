using System.Collections.Generic;

using UniRx;

public class UpgradesShopItemView : ShopItemView
{
    private List<UpgradeShopSubItemView> _subItems;

    public IReadOnlyList<UpgradeShopSubItemView> SubItems => _subItems;

    public ReactiveCommand<UpgradeShopSubItemView> TryPurchase = new ReactiveCommand<UpgradeShopSubItemView>();

    public override void Initialize(BaseShopItemValuesSO values)
    {
        base.Initialize(values);
        _subItems = new List<UpgradeShopSubItemView>();
    }

    public UpgradeShopSubItemView CreateSubItem(ShopSubItemView prefab, int price)
    {
        UpgradeShopSubItemView subItem = Instantiate(prefab.GetComponent<UpgradeShopSubItemView>(), grid.transform);
        subItem.Initialize(price);
        subItem.TryPurchase.Subscribe(clicked => TryPurchase.Execute(clicked.GetComponent<UpgradeShopSubItemView>()));

        _subItems.Add(subItem);

        return subItem;
    }
}
