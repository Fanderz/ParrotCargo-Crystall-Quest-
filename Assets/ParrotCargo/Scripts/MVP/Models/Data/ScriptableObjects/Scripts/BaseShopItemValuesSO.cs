using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class BaseShopItemValuesSO : ScriptableObject
{
    [SerializeField] protected TypeShopItem itemType;
    [SerializeField] protected string ruItemHeaderText;
    [SerializeField] protected string enItemHeaderText;
    [SerializeField] protected string trItemHeaderText;
    [SerializeField] protected int childItemsCount;
    [SerializeField] protected int defaultPurchasedCount;
    [SerializeField] private int _defaultActiveCount;
    [SerializeField] protected ShopItemView prefab;
    [SerializeField] protected ShopSubItemView childItemPrefab;
    [SerializeField] protected List<BaseShopObjectSO> shopObject;

    public TypeShopItem ItemName => itemType;
    public string ItemHeader => trItemHeaderText;
    public int ItemChildCount => childItemsCount;
    public int DefaulPurchasedCount => defaultPurchasedCount;
    public int DefaultActiveCount => _defaultActiveCount;
    public ShopItemView Prefab => prefab;
    public ShopSubItemView ChildItemPrefab => childItemPrefab;

    public int GetItemPriceAtIndex(int index)
    {
        return shopObject.ElementAt(index).Price;
    }

    public string GetItemHeader()
    {
        if (YG2.lang == "en")
            return enItemHeaderText;
        else if (YG2.lang == "tr")
            return trItemHeaderText;
        else
            return ruItemHeaderText;
    }
}
