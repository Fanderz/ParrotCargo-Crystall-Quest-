using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Sprite _filledStarSprite;

    private List<ShopItem> _shopItems;

    public IReadOnlyList<ShopItem> ShopItems => _shopItems;

    public void Initialize(List<ShopItem> shopItems)
    {
        _shopItems = shopItems;
    }

    public void SelectShip(Button button)
    {
        //ShipSelectingCommand.Execute();
    }
}
