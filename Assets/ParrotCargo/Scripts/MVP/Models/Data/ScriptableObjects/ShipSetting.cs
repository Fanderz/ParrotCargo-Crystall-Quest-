using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShipSetting", menuName = "ScriptableObject/ShipSetting")]
public class ShipSetting : ScriptableObject
{
    [Header("Id View в Магазине")]
    [SerializeField] private int _id;
    [Space]
    [Header("Image Корабля")]
    [SerializeField] private Image _shipImage;
    [Space]
    [Header("Prefabs Кораблей")]
    [SerializeField] private List<BaseShipView> _ships;

    private bool _isSelected;

    public int Id => _id;
    public bool IsSelected => _isSelected;
    public IReadOnlyList<BaseShipView> Ships => _ships;

    public void SetSelected()
    {
        _isSelected = true;
    }

    public void UnSelect()
    {
        _isSelected = false;
    }
}
