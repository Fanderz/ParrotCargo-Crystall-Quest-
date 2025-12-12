using System.Collections.Generic;

using Assets.ParrotCargo.Scripts.MVP.Models.Data;

namespace YG
{
    public partial class SavesYG
    {
        public int idSave;
        public CoinsModel coinsProgress = new CoinsModel(0);
        public PointsModel pointsProgress = new PointsModel(0);
        public SettingsModel playerSettings = new SettingsModel(1.0f, 1.0f);
        public ShopModel shopModel = new ShopModel(2, 1, new List<BaseShipView>());
        public TypeBird currentTypeBird = TypeBird.Pigeon;
    }
}
