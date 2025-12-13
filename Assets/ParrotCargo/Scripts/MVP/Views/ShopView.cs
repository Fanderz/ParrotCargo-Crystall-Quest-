using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class ShopView : MonoBehaviour
{
    [SerializeField] private ShopItem _shipCapacityItemName;
    [SerializeField] private ShopItem _palletsCapacityItemName;
    [SerializeField] private ShopItem _shipViewsItemName;
    [SerializeField] private ShopItem _parrotViewsItemName;
    [SerializeField] private List<Button> _shipCapacityStars;
    [SerializeField] private List<Button> _tempPalletsCapacityStars;
    [SerializeField] private List<Button> _shipViews;
    [SerializeField] private List<Button> _parrotViews;
    [SerializeField] private List<ShopItem> _shopItems;

    [SerializeField] private Sprite _filledStarSprite;

    private List<Image> _shipCapacityStarsImages;
    private List<Image> _tempPalletsCapacityStarsImages;
    //private List<ShopItem> _shopItems;

    public ReactiveCommand ShipStarFilledCommand = new ReactiveCommand();
    public ReactiveCommand PalletStarFilledCommand = new ReactiveCommand();
    public ReactiveCommand StarFilledCommand = new ReactiveCommand();
    public ReactiveCommand ShipSelectingCommand = new ReactiveCommand();

    public void Initialize(List<ShopItem> shopItems)
    {
        _shipCapacityStarsImages = new List<Image>();
        _tempPalletsCapacityStarsImages = new List<Image>();
        _shopItems = shopItems;
    }

    //public void SetStarsFilledOnLoad(int filledCount)
    //{
    //    for (int i = 0; i < filledCount; i++)
    //        SetStarFilled(_shopItems.Find(item => item is UpgradesShopItem).SubItems.ToList()[i].ButtonImage);
    //}

    public void SelectShip(Button button)
    {
        ShipSelectingCommand.Execute();
    }

    //private void SetShipStarFilled(Image targetImage)
    //{
    //    if (targetImage.sprite != _filledStarSprite)
    //    {
    //        SetStarFilled(targetImage);
    //        ShipStarFilledCommand.Execute();
    //    }
    //}

    //private void SetPalletStarFilled(Image targetImage)
    //{
    //    if (targetImage.sprite != _filledStarSprite)
    //    {
    //        SetStarFilled(targetImage);
    //        PalletStarFilledCommand.Execute();
    //    }
    //}

    //private void SetStarFilled(Image targetImage)
    //{
    //    if (targetImage.sprite != _filledStarSprite)
    //    {
    //        targetImage.sprite = _filledStarSprite;
    //        StarFilledCommand.Execute();
    //    }
    //}
}
