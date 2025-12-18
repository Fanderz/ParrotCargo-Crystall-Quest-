using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObject/ShopItem")]
public class ShopItemValues : ScriptableObject
{
    [SerializeField] private TypeShopItem _itemType;
    [SerializeField] private string _itemHeaderText;
    [SerializeField] private int _childItemsCount;
    [SerializeField] private ShopItem _prefab;
    [SerializeField] private ShopSubItem _childItemPrefab;
    [SerializeField] private int _childItemPrice;

    public TypeShopItem ItemName => _itemType;
    public string ItemHeader => _itemHeaderText;
    public int ItemChildCount => _childItemsCount;
    public ShopItem Prefab => _prefab;
    public ShopSubItem ChildItemPrefab => _childItemPrefab;
    public int Price => _childItemPrice;
}
