using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class UpgradeShopSubItem : ShopSubItem
{
    [SerializeField] private Sprite _filledImageSprite;

    private Button _button;
    private Image _buttonImage;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();
    public ReactiveCommand StarFilledCommand = new ReactiveCommand();

    public Button Button => _button;

    public void Initialize(ShopItemValues values)
    {
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
        price = values.Price;

        priceText.text = price.ToString();
    }

    public void SetStarFilled()
    {
        if (_buttonImage.sprite != _filledImageSprite)
        {
            _buttonImage.sprite = _filledImageSprite;
            StarFilledCommand.Execute();
        }
    }

    public override void OnPurchase()
    {
        SetStarFilled();
    }
}
