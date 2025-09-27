
using Assets.ParrotCargo.Scripts.MVP.Models.Data;

namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave;
        public PlayerProgressModel playerProgress;
        public SettingsModel playerSettings;
        public ShopModel shopModel;
    }
}
