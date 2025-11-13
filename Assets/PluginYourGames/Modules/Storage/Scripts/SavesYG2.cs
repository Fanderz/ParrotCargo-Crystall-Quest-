
using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using System;
using System.Collections.Generic;

namespace YG
{
    [Serializable]
    public partial class SavesYG
    {
        public int idSave;
        public CoinsModel coinsProgress = new CoinsModel(0);
        public PointsModel scoreProgress = new PointsModel(0);
        public SettingsModel playerSettings = new SettingsModel(1.0f, 1.0f);
        public ShopModel shopModel = new ShopModel(2, 1, new List<BaseShipView>());

        public SavesYG()
        {
            //if(YG2.saves.)
        }
    }
}
