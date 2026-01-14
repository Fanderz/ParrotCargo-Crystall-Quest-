using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] protected GridLayoutGroup grid;
    [SerializeField] protected TextMeshProUGUI headerText;

    public TypeShopItem ItemType { get; protected set; }
    public virtual void Initialize(BaseShopItemValuesSO shopItemValues)
    {
        ItemType = shopItemValues.ItemName;
        headerText.text = shopItemValues.ItemHeader;
    }
}
