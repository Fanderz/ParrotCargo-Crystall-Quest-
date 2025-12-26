using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPreviewRig : MonoBehaviour
{
    [SerializeField] private Camera _renderCamera;
    [SerializeField] private Transform _pivotTransform;

    public Camera RenderCamera => _renderCamera;
    public Transform PivotTransform => _pivotTransform;
}
