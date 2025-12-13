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

    public ReactiveCommand<int> StarFilledCommand = new ReactiveCommand<int>();

    public int Price => _price;
    public Button Button => _button;
    public Image ButtonImage => _buttonImage;

    private void Awake()
    {
        _priceText.text = _price.ToString();
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
        _button.onClick.AddListener(() => { SetStarFilled(); });
    }

    private void SetStarFilled()
    {
        if (_buttonImage.sprite != _filledImageSprite)
        {
            _buttonImage.sprite = _filledImageSprite;
            StarFilledCommand.Execute(_price);
        }
    }
}
