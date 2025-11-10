using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class ShopView : MonoBehaviour
{
    [SerializeField] private string _shipCapacityItemName;
    [SerializeField] private string _palletsCapacityItemName;
    [SerializeField] private string _shipViewsItemName;
    [SerializeField] private string _parrotViewsItemName;
    [SerializeField] private List<Button> _shipCapacityStars;
    [SerializeField] private List<Button> _tempPalletsCapacityStars;
    [SerializeField] private List<Button> _shipViews;
    [SerializeField] private List<Button> _parrotViews;

    [SerializeField] private Sprite _filledStarSprite;

    private List<Image> _shipCapacityStarsImages;
    private List<Image> _tempPalletsCapacityStarsImages;
    private List<ShopItem> _shopItems;

    public ReactiveCommand ShipStarFilledCommand = new ReactiveCommand();
    public ReactiveCommand PalletStarFilledCommand = new ReactiveCommand();
    public ReactiveCommand ShipSelectingCommand = new ReactiveCommand();

    public void Initialize(List<ShopItem> shopItems)
    {
        _shipCapacityStarsImages = new List<Image>();
        _tempPalletsCapacityStarsImages = new List<Image>();
        _shopItems = shopItems;

        var findedItems = _shopItems.Find(item => item.Name == _shipCapacityItemName);

        foreach (Button button in findedItems.Buttons)
            button.onClick.AddListener(() => { SetShipStarFilled(button.GetComponent<Image>()); });

        foreach (Button button in _shopItems.Find(item => item.Name == _palletsCapacityItemName).Buttons)
            button.onClick.AddListener(() => { SetPalletStarFilled(button.GetComponent<Image>()); });

        //foreach (var shipStar in _shipCapacityStars)
        //    _shipCapacityStarsImages.Add(shipStar.GetComponent<Image>());

        //foreach (var palletStar in _tempPalletsCapacityStars)
        //    _tempPalletsCapacityStarsImages.Add(palletStar.GetComponent<Image>());
    }

    public void SetShipStarsFilled(int filledCount)
    {
        for (int i = 0; i < filledCount; i++)
            SetShipStarFilled(_shopItems.Find(item => item.Name == _shipCapacityItemName).Images.ToList()[i]);
    }

    public void SetPalletStarsFilled(int filledCount)
    {
        for (int i = 0; i < filledCount; i++)
            SetPalletStarFilled(_shopItems.Find(item => item.Name == _palletsCapacityItemName).Images.ToList()[i]);
    }

    public void SelectShip(Button button)
    {
        ShipSelectingCommand.Execute();
    }

    private void SetShipStarFilled(Image targetImage)
    {
        if (targetImage.sprite != _filledStarSprite)
        {
            SetStarFilled(targetImage);
            ShipStarFilledCommand.Execute();
        }
    }

    private void SetPalletStarFilled(Image targetImage)
    {
        if (targetImage.sprite != _filledStarSprite)
        {
            SetStarFilled(targetImage);
            PalletStarFilledCommand.Execute();
        }
    }

    private void SetStarFilled(Image targetImage)
    {
        targetImage.sprite = _filledStarSprite;
    }
}
