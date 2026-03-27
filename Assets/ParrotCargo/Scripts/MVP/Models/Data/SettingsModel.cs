using System;

namespace Assets.ParrotCargo.Scripts.MVP.Models.Data
{
    [Serializable]
    public class SettingsModel
    {
        private float _soundValue;
        private float _musicValue;

        public float Sound { get { return _soundValue; } set { _soundValue = value; } }
        public float Music { get { return _musicValue; } set { _musicValue = value; } }

        public SettingsModel(float soundValue, float musicValue)
        {
            SetSound(soundValue);
            SetMusic(musicValue);
        }

        public void SetSound(float value)
        {
            _soundValue = value;
        }

        public void SetMusic(float value)
        {
            _musicValue = value;
        }
    }
}
