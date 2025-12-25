using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class UpgradeShopSubItemView : ShopSubItemView
{
    [SerializeField] private Sprite _filledImageSprite;

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
        SetStarFilled();
    }

    private void OnButtonClicked()
    {
        TryPurchase.Execute(this);
    }

    private void SetStarFilled()
    {
        if (_buttonImage.sprite != _filledImageSprite)
        {
            _buttonImage.sprite = _filledImageSprite;
            priceText.gameObject.SetActive(false);
            priceImage.gameObject.SetActive(false);
        }
    }
}
