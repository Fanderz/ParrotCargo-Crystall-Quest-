using UnityEngine;

public class UpgradeShopSubItemView : ShopSubItemView
{
    [SerializeField] private Sprite _filledImageSprite;

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
        SetStarFilled();
    }

    private void SetStarFilled()
    {
        if (buttonImage.sprite != _filledImageSprite)
        {
            buttonImage.sprite = _filledImageSprite;
            priceText.gameObject.SetActive(false);
            priceImage.gameObject.SetActive(false);
        }
    }
}
