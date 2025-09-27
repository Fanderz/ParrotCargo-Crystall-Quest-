using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<Button> _shipCapacityStars;
    [SerializeField] private List<Button> _tempPalletsCapacityStars;
    [SerializeField] private List<Button> _shipViews;
    [SerializeField] private List<Button> _parrotViews;

    [SerializeField] private Sprite _filledStarSprite;

    private List<Image> _shipCapacityStarsImages;
    private List<Image> _tempPalletsCapacityStarsImages;

    public ReactiveCommand ShipStarFilledCommand = new ReactiveCommand();
    public ReactiveCommand PalletStarFilledCommand = new ReactiveCommand();

    private void Awake()
    {
        foreach (var shipStar in _shipCapacityStars)
            _shipCapacityStarsImages.Add(shipStar.GetComponent<Image>());

        foreach (var palletStar in _tempPalletsCapacityStars)
            _tempPalletsCapacityStarsImages.Add(palletStar.GetComponent<Image>());
    }

    public void SetShipStarsFilled(int filledCount)
    {
        for (int i = 0; i < filledCount; i++)
            SetShipStarFilled(_shipCapacityStarsImages[i]);
    }

    public void SetPalletStarsFilled(int filledCount)
    {
        for (int i = 0; i < filledCount; i++)
            SetPalletStarFilled(_tempPalletsCapacityStarsImages[i]);
    }

    public void SetShipStarFilled(Image targetImage)
    {
        SetStarFilled(targetImage);
        ShipStarFilledCommand.Execute();
    }

    public void SetPalletStarFilled(Image targetImage)
    {
        SetStarFilled(targetImage);
        PalletStarFilledCommand.Execute();
    }

    private void SetStarFilled(Image targetImage)
    {
        targetImage.sprite = _filledStarSprite;
    }
}
