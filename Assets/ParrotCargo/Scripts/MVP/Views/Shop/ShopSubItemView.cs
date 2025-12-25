using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class ShopSubItemView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI priceText;
    [SerializeField] protected Image priceImage;

    protected int price;
    public int Price => price;

    public ReactiveCommand<ShopSubItemView> TryPurchase = new ReactiveCommand<ShopSubItemView>();

    public virtual void OnPurchase()
    {
    }
}
