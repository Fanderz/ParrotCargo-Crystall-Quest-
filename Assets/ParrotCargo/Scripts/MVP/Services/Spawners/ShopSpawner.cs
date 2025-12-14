using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [SerializeField] private List<ShopItemValues> _upgradeItemValues;
    [SerializeField] private List<ShopItemValues> _purchaseItemValues;
    [SerializeField] private Transform _upgradesParentUI;
    [SerializeField] private Transform _purchaseParentUI;

    private List<ShopItem> _items;

    public IReadOnlyList<ShopItem> ShopItems => _items;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();

    public void Spawn()
    {
        _items = new List<ShopItem>();

        SpawnItems(_upgradesParentUI, _upgradeItemValues);
        SpawnItems(_purchaseParentUI, _purchaseItemValues);
    }

    private void SpawnItems(Transform parent, List<ShopItemValues> items)
    {
        foreach (ShopItemValues itemValues in items)
        {
            ShopItem shopItem = SpawnItem(parent, itemValues);
            shopItem.Initialize(itemValues);
            //Subscribes(shopItem);
            _items.Add(shopItem);
        }
    }

    //private void Subscribes(ShopItem item)
    //{
    //    if(item is UpgradesShopItem)
    //    {
    //        UpgradesShopItem newItem = (UpgradesShopItem)item;

    //        foreach (UpgradeShopSubItem subItem in newItem.SubItems)
    //            subItem.PurchaseCommand.Subscribe(price => { PurchaseCommand.Execute(price); });
    //    }
    //}

    private ShopItem SpawnItem(Transform parent, ShopItemValues values)
    {
        return Instantiate(values.Prefab, parent); ;
    }
}
