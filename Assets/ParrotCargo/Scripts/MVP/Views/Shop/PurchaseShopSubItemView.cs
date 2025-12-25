using UnityEngine;
using UnityEngine.UI;

public class PurchaseShopSubItemView : ShopSubItemView
{
    private Button _button;
    private Image _buttonImage;

    public void Initialize(int priceValue)
    {
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();

        price = priceValue;
        priceText.text = price.ToString();

        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnButtonClicked);
    }
    public override void OnPurchase()
    {
        SetPurchased();
    }

    private void OnButtonClicked()
    {
        TryPurchase.Execute(this);
    }

    private void SetPurchased()
    {
        priceText.gameObject.SetActive(false);
        priceImage.gameObject.SetActive(false);
    }
}
