using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPreviewService : MonoBehaviour
{
    //[SerializeField] private Transform _previewPivot;

    //private GameObject _currentPreview;

    //public void ShowPreview(GameObject previewPrefab)
    //{
    //    Clear();

    //    _currentPreview = Instantiate(previewPrefab, _previewPivot);
    //    _currentPreview.transform.localPosition = Vector3.zero;
    //    _currentPreview.transform.localRotation = Quaternion.identity;
    //    _currentPreview.transform.localScale = Vector3.one;

    //    SetLayerRecursively(_currentPreview, LayerMask.NameToLayer("ShopPreview"));
    //}

    //public void HidePreview()
    //{
    //    Clear();
    //}

    //private void Clear()
    //{
    //    if (_currentPreview != null)
    //        Destroy(_currentPreview);
    //}

    //private void SetLayerRecursively(GameObject obj, int layer)
    //{
    //    obj.layer = layer;
    //    foreach (Transform child in obj.transform)
    //        SetLayerRecursively(child.gameObject, layer);
    //}
}
