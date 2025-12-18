using System.Collections.Generic;

using Assets.ParrotCargo.Scripts.MVP.Models.Data;

namespace YG
{
    public partial class SavesYG
    {
        public int idSave;
        public CoinsModel coinsProgress;
        public PointsModel pointsProgress;
        public SettingsModel playerSettings;
        public ShopModel shopModel;
        public TypeBird currentTypeBird = TypeBird.Pigeon;
    }
}
