using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [SerializeField] private List<ShopItemValues> _upgradeItemValues;
    [SerializeField] private List<ShopItemValues> _purchaseItemValues;
    [SerializeField] private Transform _upgradesParentUI;
    [SerializeField] private Transform _purchaseParentUI;

    private List<ShopItemView> _items;

    public IReadOnlyList<ShopItemView> ShopItems => _items;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();

    public void Spawn()
    {
        _items = new List<ShopItemView>();

        SpawnItems(_upgradesParentUI, _upgradeItemValues);
        SpawnItems(_purchaseParentUI, _purchaseItemValues);
    }

    private void SpawnItems(Transform parent, List<ShopItemValues> items)
    {
        foreach (ShopItemValues itemValues in items)
        {
            ShopItemView shopItem = SpawnItem(parent, itemValues);
            shopItem.Initialize(itemValues);
            _items.Add(shopItem);
        }
    }

    private ShopItemView SpawnItem(Transform parent, ShopItemValues values)
    {
        return Instantiate(values.Prefab, parent); ;
    }
}
