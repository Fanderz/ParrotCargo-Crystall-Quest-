using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObject/Ship")]
public class ShipSO : BaseShopObjectSO
{
    [SerializeField] private TypeShip _typeShip;
    [SerializeField] private List<BaseShipView> _shipPrefabs;

    public TypeShip TypeShip => _typeShip;
    public List<BaseShipView> ShipPrefabs => _shipPrefabs;
}
