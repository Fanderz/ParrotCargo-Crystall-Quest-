using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UpgradesShopItem : ShopItem
{
    [SerializeField] private int _upgradesCnt;

    private List<UpgradeShopSubItem> _subItems;

    public IReadOnlyList<UpgradeShopSubItem> SubItems => _subItems;

    public override void Initialize(ShopItemValues values)
    {
        base.Initialize(values);
        _subItems = new List<UpgradeShopSubItem>();

        for (int i = 0; i < _upgradesCnt; i++)
        {
            UpgradeShopSubItem subItem = Instantiate((UpgradeShopSubItem)values.ChildItemPrefab, grid.transform);
            _subItems.Add(subItem);
        }            
    }

    //public void SetStarsFilledOnLoad(int filledCount)
    //{
    //    for (int i = 0; i < filledCount; i++)
    //        SetStarFilled(_shopItems.Find(item => item is UpgradesShopItem).SubItems.ToList()[i].ButtonImage);
    //}
}
