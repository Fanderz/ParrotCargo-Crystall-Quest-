using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopSubItem : ShopSubItem
{
    [SerializeField] private int _price;
    [SerializeField] private Sprite _filledImageSprite;
    [SerializeField] private TextMeshProUGUI _priceText;

    private Button _button;
    private Image _buttonImage;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();
    public ReactiveCommand StarFilledCommand = new ReactiveCommand();

    public int Price => _price;
    public Button Button => _button;
    public Image ButtonImage => _buttonImage;

    private void Awake()
    {
        _priceText.text = _price.ToString();
        //_button.onClick.AddListener(() => { OnPurchase(); });
    }

    public void Initialize()
    {
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
    }

    public void SetStarFilled()
    {
        if (_buttonImage.sprite != _filledImageSprite)
        {
            _buttonImage.sprite = _filledImageSprite;
            StarFilledCommand.Execute();
        }
    }

    public void OnPurchase()
    {
        SetStarFilled();
        //PurchaseCommand.Execute(_price);
    }
}
