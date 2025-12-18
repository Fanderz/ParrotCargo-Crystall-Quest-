using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected GridLayoutGroup grid;
    [SerializeField] protected TextMeshProUGUI headerText;

    public TypeShopItem ItemType { get; protected set; }
    public virtual void Initialize(ShopItemValues shopItemValues)
    {
        ItemType = shopItemValues.ItemName;
        headerText.text = shopItemValues.ItemHeader;
    }
}
