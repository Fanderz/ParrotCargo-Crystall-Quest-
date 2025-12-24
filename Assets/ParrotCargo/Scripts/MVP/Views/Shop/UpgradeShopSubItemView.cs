using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class UpgradeShopSubItemView : ShopSubItemView
{
    [SerializeField] private Sprite _filledImageSprite;

    private Button _button;
    private Image _buttonImage;

    public void Initialize(ShopItemValues values)
    {
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();

        price = values.Price;
        priceText.text = price.ToString();

        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        TryPurchase.Execute(this);
    }

    public override void OnPurchase()
    {
        SetStarFilled();
    }

    public void SetStarFilled()
    {
        if (_buttonImage.sprite != _filledImageSprite)
        {
            _buttonImage.sprite = _filledImageSprite;
            //_button.interactable = false;
        }
    }
}
