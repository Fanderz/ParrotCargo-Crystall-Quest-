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

    public void Initialize(ShopItemValues values)
    {
        _view.Initialize(values);

        //if (_view is UpgradesShopItemView upgradesItemView)
        //{
        //    foreach (var item in _model)
        //    {
        //        UpgradeShopSubItemView subItem = upgradesItemView.CreateSubItem(values.ChildItemPrefab, values.Price);
        //        ShopSubItemPresenter subItemPresenter = new ShopSubItemPresenter(subItem, item);
        //        subItemPresenter.Initialize();
        //        subItemPresenter.PurchaseClicked.Subscribe(clicked => 
        //        Purchasing.Execute(clicked));

        //        _subItemPresenters.Add(subItemPresenter);
        //    }
        //}

        //if (_view is PurchaseShopItemView purchaseItemView)
        //{
        //    for (int i = 0; i < _model.Count; i++)
        //    {
        //        var saveData = _model[i];
        //        var subItem = purchaseItemView.CreateSubItem(values.ChildItemPrefab, values.Price);

        //        SetupPurchaseSubItemPreview(subItem, values, i);

        //        var presenter = new ShopSubItemPresenter(subItem, saveData);
        //        presenter.Initialize();
        //        presenter.PurchaseClicked.Subscribe(clicked => 
        //        Purchasing.Execute(clicked));

        //        _subItemPresenters.Add(presenter);
        //    }
        //}

        foreach (var item in _model)
        {
            ShopSubItemView itemView;

            if (_view is UpgradesShopItemView)
            {
                itemView = _view.GetComponent<UpgradesShopItemView>().CreateSubItem(values.ChildItemPrefab, values.Price);
            }
            else
            {
                itemView = _view.GetComponent<PurchaseShopItemView>().CreateSubItem(values.ChildItemPrefab, values.Price);
                SetupPurchaseSubItemPreview(itemView.GetComponent<PurchaseShopSubItemView>(), values, _model.IndexOf(item));
            }

            ShopSubItemPresenter subItemPresenter = new ShopSubItemPresenter(itemView, item);
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

    private void SetupPurchaseSubItemPreview(PurchaseShopSubItemView subItem, ShopItemValues values, int index)
    {
        int layer = LayerMask.NameToLayer($"ShopPreview{index}");

        ShopPreviewRig rig = Object.Instantiate(values.PreviewRigPrefab, subItem.transform);
        GameObject preview = Object.Instantiate(values.PreviewPrefabs[index], rig.PivotTransform, false);

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
