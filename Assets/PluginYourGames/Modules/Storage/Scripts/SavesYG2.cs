using Assets.ParrotCargo.Scripts.MVP.Models.Data;

namespace YG
{
    public partial class SavesYG
    {
        public int idSave;
        public CoinsModel coinsProgress;
        public PointsModel pointsProgress;
        public SettingsModel playerSettings;
        public ShopSaveModel shopModel;
        public TypeBird currentTypeBird = TypeBird.Parrot;
        public TypeShip currentTypeShip = TypeShip.Pirate;
        public bool isFirstGame;
        public int currentNumberLevel = 1;
        public int maxOppenedNumberLevel = 1;
    }
}
