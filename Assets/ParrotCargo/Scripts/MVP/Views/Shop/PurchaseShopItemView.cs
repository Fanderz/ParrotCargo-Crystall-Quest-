using System.Collections.Generic;

using UniRx;
using YG.LanguageLegacy;

public class PurchaseShopItemView : ShopItemView
{
    private List<PurchaseShopSubItemView> _subItems;

    public IReadOnlyList<PurchaseShopSubItemView> SubItems => _subItems;

    public ReactiveCommand<PurchaseShopSubItemView> TryPurchase = new ReactiveCommand<PurchaseShopSubItemView>();

    public override void Initialize(BaseShopItemValuesSO values)
    {
        base.Initialize(values);
        languageYG = headerText.GetComponent<LanguageYG>();
        _subItems = new List<PurchaseShopSubItemView>();

        languageYG.text = itemValues.ItemHeader;
        languageYG.textMPComponent.SetText(itemValues.ItemHeader);
        languageYG.AssignTranslate();
        languageYG.Translate(languageYG.countLang);
        languageYG.AssignTranslate();
    }

    public PurchaseShopSubItemView CreateSubItem(ShopSubItemView prefab, int price)
    {
        PurchaseShopSubItemView subItem = Instantiate(prefab, grid.transform).GetComponent<PurchaseShopSubItemView>();
        subItem.Initialize(price);
        subItem.TryPurchase.Subscribe(clicked => TryPurchase.Execute(clicked.GetComponent<PurchaseShopSubItemView>()));

        _subItems.Add(subItem);

        return subItem;
    }
}
