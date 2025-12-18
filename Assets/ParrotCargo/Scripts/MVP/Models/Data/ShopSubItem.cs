using UnityEngine;
using TMPro;

public class ShopSubItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI priceText;

    protected int price;

    public int Price => price;

    public virtual void OnPurchase()
    { }
}
