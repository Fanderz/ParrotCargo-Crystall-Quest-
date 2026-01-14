using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseShopItemValuesSO : ScriptableObject
{
    [SerializeField] protected TypeShopItem itemType;
    [SerializeField] protected string itemHeaderText;
    [SerializeField] protected int childItemsCount;
    [SerializeField] protected int defaultPurchasedCount;
    [SerializeField] private int _defaultActiveCount;
    [SerializeField] protected ShopItemView prefab;
    [SerializeField] protected ShopSubItemView childItemPrefab;
    [SerializeField] protected List<BaseShopObjectSO> shopObject;

    public TypeShopItem ItemName => itemType;
    public string ItemHeader => itemHeaderText;
    public int ItemChildCount => childItemsCount;
    public int DefaulPurchasedCount => defaultPurchasedCount;
    public int DefaultActiveCount => _defaultActiveCount;
    public ShopItemView Prefab => prefab;
    public ShopSubItemView ChildItemPrefab => childItemPrefab;

    public int GetItemPriceAtIndex(int index)
    {
        return shopObject.ElementAt(index).Price;
    }
}
