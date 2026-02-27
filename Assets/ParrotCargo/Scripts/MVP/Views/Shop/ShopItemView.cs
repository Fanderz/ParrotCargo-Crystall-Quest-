using UnityEngine;
using UnityEngine.UI;

using TMPro;
using YG.LanguageLegacy;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] protected GridLayoutGroup grid;
    [SerializeField] protected TextMeshProUGUI headerText;

    protected BaseShopItemValuesSO itemValues;

    public TypeShopItem ItemType { get; protected set; }
    public virtual void Initialize(BaseShopItemValuesSO shopItemValues)
    {
        itemValues = shopItemValues;

        ItemType = shopItemValues.ItemName;
        headerText.text = shopItemValues.ItemHeader;
    }
}
