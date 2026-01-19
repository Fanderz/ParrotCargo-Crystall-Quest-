using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;
using UniRx;
using YG;

public class ShopService : BaseService
{
    [SerializeField] private ShopView _view;
    [SerializeField] private ShopSpawner _shopSpawner;

    [Inject] PlayerProgressService _playerProgressService;

    private ShopPresenter _shopPresenter;
    private ShopModel _shopModel;

    public IReadOnlyList<BaseShopItemValuesSO> UpgradeItemsSettings => _shopSpawner.UpgradeItemSettings;
    public IReadOnlyList<BaseShopItemValuesSO> PurchaseItemsSettings => _shopSpawner.PurchaseItemsSettings;
    public ShopModel Model => _shopModel;

    public override void Initialize()
    {
        _shopModel = new ShopModel(YG2.saves.shopModel);

        _shopSpawner.Spawn();

        _shopPresenter = new ShopPresenter(_shopModel, _view, _shopSpawner.ShopItems.ToList());
        _shopPresenter.PurchaseCommand.Subscribe(price => _playerProgressService.DecreaseOnPurchase(price)).AddTo(this);
        _shopPresenter.Initialize();
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
