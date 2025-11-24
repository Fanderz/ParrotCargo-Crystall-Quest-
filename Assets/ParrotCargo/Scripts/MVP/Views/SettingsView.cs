using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Assets.ParrotCargo.Scripts.MVP.Views
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Button _saveButton;

        public ReactiveCommand<float> SoundChanged = new ReactiveCommand<float>();
        public ReactiveCommand<float> MusicChanged = new ReactiveCommand<float>();

        private void Awake()
        {
            //if (YG2.saves.playerSettings != null)
            //{
            //    _soundSlider.value = YG2.saves.playerSettings.Sound;
            //    _musicSlider.value = YG2.saves.playerSettings.Music;
            //}
        }

        public void Initialize(SettingsModel model)
        {
            _soundSlider.value = YG2.saves.playerSettings.Sound;
            _musicSlider.value = YG2.saves.playerSettings.Music;
        }

        public void SetSound()
        {
            SoundChanged.Execute(_soundSlider.value);
        }

        public void SetMusic()
        {
            MusicChanged.Execute(_musicSlider.value);
        }

        public void Save()
        {
            YG2.saves.playerSettings.Sound = _soundSlider.value;
            YG2.saves.playerSettings.Music = _musicSlider.value;

            YG2.SaveProgress();
        }
    }
}
