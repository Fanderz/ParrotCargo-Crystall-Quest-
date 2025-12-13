using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObject/ShopItem")]
public class ShopItemValues : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemHeaderText;
    [SerializeField] private int _childItemsCount;
    [SerializeField] private ShopItem _prefab;
    [SerializeField] private ShopSubItem _childItemPrefab;
    [SerializeField] private int _childItemPrice;

    public string ItemName => _itemName;
    public string ItemHeader => _itemHeaderText;
    public int ItemChildCount => _childItemsCount;
    public ShopItem Prefab => _prefab;
    public ShopSubItem ChildItemPrefab => _childItemPrefab;
}
