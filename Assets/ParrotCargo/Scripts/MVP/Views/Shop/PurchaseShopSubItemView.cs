using UnityEngine;
using UnityEngine.UI;

public class PurchaseShopSubItemView : ShopSubItemView
{
    [SerializeField] private RawImage _modelRaw;

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

    public void BindPreview(RenderTexture rt)
    {
        if (_modelRaw == null) 
            return;

        _modelRaw.texture = rt;
        _modelRaw.enabled = (rt != null);
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
