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

        for (int i = 0; i < values.ItemChildCount; i++)
        {
            UpgradeShopSubItemView subItem = Instantiate((UpgradeShopSubItemView)values.ChildItemPrefab, grid.transform);
            subItem.Initialize(values);
            subItem.TryPurchase.Subscribe(clicked => TryPurchase.Execute((UpgradeShopSubItemView)clicked));
            //TryPurchase.Execute(subItem));
            _subItems.Add(subItem);
        }
    }

    public void SetStarsFilledOnLoad(int filledCount)
    {
        for (int i = 0; i < filledCount; i++)
            _subItems[i].SetStarFilled();
    }  
}
