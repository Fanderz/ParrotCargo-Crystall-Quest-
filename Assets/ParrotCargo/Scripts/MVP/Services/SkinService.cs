using System.Collections.Generic;

using UnityEngine;

using YG;
using UniRx;
using Zenject;

public class SkinService : BaseService
{
    [Header("Settings")]
    [SerializeField] private List<Bird> _birdsPrefab;
    [SerializeField] private List<ShipSO> _shipsPrefab;

    [Inject] private ShopService _shopService;

    public Bird CurrentBird { get; private set; }
    public ShipSO CurrentShip { get; private set; }

    public override void Initialize()
    {
        var savedCurrentBird = YG2.saves.currentTypeBird;
        var savedCurrentShip = YG2.saves.currentTypeShip;
        CurrentBird = _birdsPrefab.Find(bird => bird.TypeBird == savedCurrentBird);
        CurrentShip = _shipsPrefab.Find(ship => ship.TypeShip == savedCurrentShip);

        _shopService.Model.PurchaseItemActivated.Subscribe(SetSkin).AddTo(this);
    }

    private void SetSkin((int index, TypeShopItem itemType) input)
    {
        if (input.itemType == TypeShopItem.ParrotPurchase)
            YG2.saves.currentTypeBird = (TypeBird)input.index;

        if (input.itemType == TypeShopItem.ShipPurchase)
            YG2.saves.currentTypeShip = (TypeShip)input.index;
    }
}
