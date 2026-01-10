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

    public Bird CurrentBird => _birdsPrefab.Find(bird => bird.TypeBird == YG2.saves.currentTypeBird);
    public ShipSO CurrentShip => _shipsPrefab.Find(ship => ship.TypeShip == YG2.saves.currentTypeShip);

    public override void Initialize()
    {
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
