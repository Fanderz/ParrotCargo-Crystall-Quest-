using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Sprite _filledStarSprite;

    private List<ShopItemView> _shopItems;

    public IReadOnlyList<ShopItemView> ShopItems => _shopItems;

    public void Initialize(List<ShopItemView> shopItems)
    {
        _shopItems = shopItems;
    }

    public void SelectShip(Button button)
    {
        //ShipSelectingCommand.Execute();
    }
}
