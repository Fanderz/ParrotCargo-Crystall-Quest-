using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShopObjectSO : ScriptableObject
{
    [SerializeField] protected int price;
    [SerializeField] protected GameObject previewPrefab;

    public int Price => price;
    public GameObject PreviewPrefab => previewPrefab;
}
