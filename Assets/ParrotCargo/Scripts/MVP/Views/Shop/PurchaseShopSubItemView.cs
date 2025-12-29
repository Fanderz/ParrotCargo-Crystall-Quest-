using UnityEngine;
using UnityEngine.UI;

public class PurchaseShopSubItemView : ShopSubItemView
{
    [SerializeField] private RawImage _modelRaw;

    public override void Initialize(int priceValue)
    {
        base.Initialize(priceValue);
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

    private void SetPurchased()
    {
        priceText.gameObject.SetActive(false);
        priceImage.gameObject.SetActive(false);
    }
}
