using System.Collections.Generic;

using UniRx;

public class UpgradesShopItem : ShopItem
{
    private List<UpgradeShopSubItem> _subItems;

    public IReadOnlyList<UpgradeShopSubItem> SubItems => _subItems;

    public ReactiveCommand<UpgradeShopSubItem> TryPurchase = new ReactiveCommand<UpgradeShopSubItem>();

    public override void Initialize(ShopItemValues values)
    {
        base.Initialize(values);
        _subItems = new List<UpgradeShopSubItem>();

        for (int i = 0; i < values.ItemChildCount; i++)
        {
            UpgradeShopSubItem subItem = Instantiate((UpgradeShopSubItem)values.ChildItemPrefab, grid.transform);
            subItem.Initialize(values);
            subItem.Button.onClick.AddListener(() => TryPurchase.Execute(subItem));
            _subItems.Add(subItem);
        }
    }

    public void SetStarsFilledOnLoad(int filledCount)
    {
        for (int i = 0; i < filledCount; i++)
            _subItems[i].SetStarFilled();
    }  
}
