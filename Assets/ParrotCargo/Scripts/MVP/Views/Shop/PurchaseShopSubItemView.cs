using UnityEngine;
using UnityEngine.UI;

public class PurchaseShopSubItemView : ShopSubItemView
{
    [SerializeField] private RawImage _modelRaw;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _unactiveColor;

    private Image _image;

    public override void Initialize(int priceValue)
    {
        base.Initialize(priceValue);

        _image = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveAllListeners();
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

    public void SetActiveView()
    {
        SetColor(_activeColor);
    }

    public void SetUnActiveView()
    {
        SetColor (_unactiveColor);
    }

    private void SetColor(Color color)
    {
        _image.color = color;
    }

    private void SetPurchased()
    {
        priceText.gameObject.SetActive(false);
        priceImage.gameObject.SetActive(false);
    }
}
