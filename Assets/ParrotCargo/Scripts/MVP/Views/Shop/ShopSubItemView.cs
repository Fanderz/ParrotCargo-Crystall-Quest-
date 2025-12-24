using UnityEngine;
using TMPro;
using UniRx;

public class ShopSubItemView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI priceText;

    protected int price;
    public int Price => price;

    public ReactiveCommand<ShopSubItemView> TryPurchase = new ReactiveCommand<ShopSubItemView>();

    public virtual void OnPurchase()
    {
    }
}
