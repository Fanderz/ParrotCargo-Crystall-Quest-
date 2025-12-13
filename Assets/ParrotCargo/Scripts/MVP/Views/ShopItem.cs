using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected GridLayoutGroup grid;
    [SerializeField] protected TextMeshProUGUI headerText;

    public virtual void Initialize(ShopItemValues shopItemValues)
    {
        headerText.text = shopItemValues.ItemHeader;
    }
}
