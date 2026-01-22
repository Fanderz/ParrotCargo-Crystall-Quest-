using System.Collections.Generic;
using UniRx;
using UnityEngine;
using YG;

public class ShopSpawner : MonoBehaviour
{
    [SerializeField] private Transform _upgradesParentUI;
    [SerializeField] private Transform _purchaseParentUI;

    [SerializeField] private List<BaseShopItemValuesSO> _upgradeItemValues;
    [SerializeField] private List<BaseShopItemValuesSO> _purchaseItemValues;

    private List<Transform> _previewPivots;
    private List<RenderTexture> _previewTextures;

    private List<ShopItemPresenter> _presenters;

    public IReadOnlyList<BaseShopItemValuesSO> UpgradeItemSettings => _upgradeItemValues;
    public IReadOnlyList<BaseShopItemValuesSO> PurchaseItemsSettings => _purchaseItemValues;
    public IReadOnlyList<ShopItemPresenter> ShopItems => _presenters;

    public ReactiveCommand<int> PurchaseCommand = new ReactiveCommand<int>();

    public void Spawn()
    {
        _presenters = new List<ShopItemPresenter>();
        _previewPivots = new List<Transform>();
        _previewTextures = new List<RenderTexture>();

        SpawnItems(_upgradesParentUI, _upgradeItemValues);
        SpawnItems(_purchaseParentUI, _purchaseItemValues);
    }

    private void SpawnItems(Transform parent, List<BaseShopItemValuesSO> items)
    {
        foreach (BaseShopItemValuesSO itemValues in items)
        {
            List<ShopSaveData> saveData = itemValues.ItemName switch
            {
                TypeShopItem.PalletUpgrade => YG2.saves.shopModel.upgradeItems.FindAll(item => item.Type == TypeShopItem.PalletUpgrade),
                TypeShopItem.ShipUpgrade => YG2.saves.shopModel.upgradeItems.FindAll(item => item.Type == TypeShopItem.ShipUpgrade),
                TypeShopItem.ParrotPurchase => YG2.saves.shopModel.purchaseItems.FindAll(item => item.Type == TypeShopItem.ParrotPurchase),
                TypeShopItem.ShipPurchase => YG2.saves.shopModel.purchaseItems.FindAll(item => item.Type == TypeShopItem.ShipPurchase),
                _ => new List<ShopSaveData> { new NullableShopSaveData() }
            };

            ShopItemView shopItem = SpawnItem(parent, itemValues);
            ShopItemPresenter presenter = new ShopItemPresenter(shopItem, saveData);
            presenter.Initialize(itemValues);

            _presenters.Add(presenter);
        }
    }

    private ShopItemView SpawnItem(Transform parent, BaseShopItemValuesSO values)
    {
        return Instantiate(values.Prefab, parent);
    }
}
