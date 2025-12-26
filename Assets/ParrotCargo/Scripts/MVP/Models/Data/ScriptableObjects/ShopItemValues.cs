using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemView", menuName = "ScriptableObject/ShopItemView")]
public class ShopItemValues : ScriptableObject
{
    [SerializeField] private TypeShopItem _itemType;
    [SerializeField] private string _itemHeaderText;
    [SerializeField] private int _childItemsCount;
    [SerializeField] private int _defaultPurchasedCount;
    [SerializeField] private ShopItemView _prefab;
    [SerializeField] private ShopSubItemView _childItemPrefab;
    [SerializeField] private ShopPreviewRig _previewRigPrefab;
    [SerializeField] private List<RenderTexture> _previewRenderTextures;
    [SerializeField] private List<GameObject> _previewPrefabs;
    [SerializeField] private int _childItemPrice;

    public TypeShopItem ItemName => _itemType;
    public string ItemHeader => _itemHeaderText;
    public int ItemChildCount => _childItemsCount;
    public int DefaulPurchasedCount => _defaultPurchasedCount;
    public ShopItemView Prefab => _prefab;
    public ShopSubItemView ChildItemPrefab => _childItemPrefab;
    public ShopPreviewRig PreviewRigPrefab => _previewRigPrefab;
    public int Price => _childItemPrice;
    public IReadOnlyList<GameObject> PreviewPrefabs => _previewPrefabs;
    public IReadOnlyList<RenderTexture> PreviewRenderTextures => _previewRenderTextures;
}
