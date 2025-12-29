using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class ShopSubItemView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI priceText;
    [SerializeField] protected Image priceImage;

    protected int price;

    protected Button button;
    protected Image buttonImage;

    public int Price => price;
    public Button Button => button;


    public ReactiveCommand<ShopSubItemView> TryPurchase = new ReactiveCommand<ShopSubItemView>();

    public virtual void Initialize(int priceValue)
    {
        price = priceValue;
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        priceText.text = price.ToString();
    }

    public virtual void OnPurchase()
    {
    }
}
