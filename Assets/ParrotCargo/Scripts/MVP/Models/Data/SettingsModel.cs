using System;
using UniRx;

namespace Assets.ParrotCargo.Scripts.MVP.Models.Data
{
    [Serializable]
    public class SettingsModel
    {
        private float _soundValue = 1.0f;
        private float _musicValue = 1.0f;

        public float Sound => _soundValue;
        public float Music => _musicValue;

        public ReactiveCommand<float> SoundChanged = new ReactiveCommand<float>();
        public ReactiveCommand<float> MusicChanged = new ReactiveCommand<float>();

        public SettingsModel(float soundValue, float musicValue)
        {
            _soundValue = soundValue;
            _musicValue = musicValue;
        }

        public void AllChanged()
        {
            SoundChanged.Execute(_soundValue);
            MusicChanged.Execute(_musicValue);
        }

        public void SetSound(float value)
        {
            _soundValue = value;
            SoundChanged.Execute(_soundValue);
        }

        public void SetMusic(float value)
        {
            _musicValue = value;
            MusicChanged.Execute(_musicValue);
        }
    }
}
