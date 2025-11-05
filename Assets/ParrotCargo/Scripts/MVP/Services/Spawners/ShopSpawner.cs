using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    //[SerializeField] private ShopItem _upgradesItemPrefab;
    //[SerializeField] private ShopItem _purchasesItemPrefab;
    [SerializeField] private List<ShopItemValues> _upgradeItemValues;
    [SerializeField] private List<ShopItemValues> _purchaseItemValues;
    [SerializeField] private Transform _upgradesParentUI;
    [SerializeField] private Transform _purchaseParentUI;

    private List<ShopItem> _items;

    public IReadOnlyList<ShopItem> ShopItems => _items;

    public void Spawn()
    {
        _items = new List<ShopItem>();

        foreach (var item in _upgradeItemValues)
        {
            var obj = SpawnItem(_upgradesParentUI, item);
            _items.Add(obj);
        }

        foreach (var item in _purchaseItemValues)
        {
            var obj = SpawnItem(_purchaseParentUI, item);
            _items.Add(obj);
        }
    }

    private ShopItem SpawnItem(Transform parent, ShopItemValues values)
    {
        ShopItem item = Instantiate(values.Prefab, _purchaseParentUI);
        item.Initialize(values);

        return item;
    }
}
