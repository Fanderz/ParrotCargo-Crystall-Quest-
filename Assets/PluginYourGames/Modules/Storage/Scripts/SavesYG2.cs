
using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using System;
using System.Collections.Generic;
using UniRx.Diagnostics;

namespace YG
{
    public partial class SavesYG
    {
        public int idSave;
        public CoinsModel coinsProgress = new CoinsModel(0);
        public PointsModel pointsProgress = new PointsModel(0);
        public SettingsModel playerSettings = new SettingsModel(1.0f, 1.0f);
        public ShopModel shopModel = new ShopModel(
            new List<UpgradeShopItemModel> { new UpgradeShopItemModel(2), new UpgradeShopItemModel(1) }, 
            new List<BaseShipView>());
    }
}
