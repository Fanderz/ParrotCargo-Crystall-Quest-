using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemView", menuName = "ScriptableObject/ShopItemView")]
public class PurchaseShopItemValues : BaseShopItemValuesSO
{
    [SerializeField] private ShopPreviewRig _previewRigPrefab;
    [SerializeField] private List<RenderTexture> _previewRenderTextures;

    public ShopPreviewRig PreviewRigPrefab => _previewRigPrefab;
    public IReadOnlyList<RenderTexture> PreviewRenderTextures => _previewRenderTextures;

    public GameObject GetPreviewPrefabAtIndex(int index)
    {
        return shopObject.ElementAt(index).PreviewPrefab;
    }
}
