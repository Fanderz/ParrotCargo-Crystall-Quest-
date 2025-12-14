using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using YG;
using static UnityEditor.Progress;

public class UpgradesShopItem : ShopItem
{
    //[SerializeField] private int _upgradesCnt;

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
            subItem.Initialize();
            //subItem.PurchaseCommand.Subscribe(purchase => );
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
