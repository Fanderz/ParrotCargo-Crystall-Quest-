using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ShopItemPresenter
{
    private readonly ShopItemView _view;
    private readonly List<ShopSaveData> _model;
    private readonly TypeShopItem _type;

    private List<ShopSubItemPresenter> _subItemPresenters;

    public ReactiveCommand<ShopSubItemPresenter> Purchasing = new ReactiveCommand<ShopSubItemPresenter>();

    public ShopItemPresenter(ShopItemView view, List<ShopSaveData> model)
    {
        _subItemPresenters = new List<ShopSubItemPresenter>();

        _view = view;
        _model = model;
        _type = view.ItemType;
    }

    public TypeShopItem ItemType => _type;
    public IReadOnlyList<ShopSubItemPresenter> SubItemPresenters => _subItemPresenters;

    public void Initialize(BaseShopItemValuesSO values)
    {
        _view.Initialize(values);

        for (int i = 0; i < _model.Count; i++)
        {
            ShopSubItemView itemView;

            if (_view is UpgradesShopItemView)
            {
                itemView = _view.GetComponent<UpgradesShopItemView>().CreateSubItem(values.ChildItemPrefab, values.GetItemPriceAtIndex(i));
            }
            else
            {
                itemView = _view.GetComponent<PurchaseShopItemView>().CreateSubItem(values.ChildItemPrefab, values.GetItemPriceAtIndex(i));
                SetupPurchaseSubItemPreview(itemView.GetComponent<PurchaseShopSubItemView>(), (PurchaseShopItemValues)values, i);
            }

            ShopSubItemPresenter subItemPresenter = new ShopSubItemPresenter(itemView, _model[i]);
            subItemPresenter.Initialize();
            subItemPresenter.PurchaseClicked.Subscribe(clicked => Purchasing.Execute(clicked));

            _subItemPresenters.Add(subItemPresenter);
        }

    }

    public void OnModelChanged(ShopSaveData data)
    {
        ShopSubItemPresenter subItem = _subItemPresenters.Find(presenter => presenter.SaveData == data);

        if (subItem == null)
            return;

        subItem.SetPurchased();
    }

    private void SetupPurchaseSubItemPreview(PurchaseShopSubItemView subItem, PurchaseShopItemValues values, int index)
    {
        int layer = LayerMask.NameToLayer($"ShopPreview{index}");

        ShopPreviewRig rig = Object.Instantiate(values.PreviewRigPrefab, subItem.transform);
        GameObject preview = Object.Instantiate(values.GetPreviewPrefabAtIndex(index), rig.PivotTransform, false);

        rig.name = $"PreviewRig_{index}";
        rig.transform.position = new Vector3(10000, 10000, 10000);
        rig.RenderCamera.targetTexture = values.PreviewRenderTextures[index];
        rig.RenderCamera.cullingMask = 1 << layer;

        SetLayerRecursive(preview, layer);
        subItem.BindPreview(values.PreviewRenderTextures[index]);
    }


    private void SetLayerRecursive(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform t in obj.transform)
            SetLayerRecursive(t.gameObject, layer);
    }
}
