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

        private void Start()
        {
            _saveButton.onClick.AddListener(YG2.SaveProgress);
        }

        public void SetSound(float value)
        {
            _soundSlider.value = value;
            SoundChanged.Execute(_soundSlider.value);
        }

        public void SetMusic(float value)
        {
            _musicSlider.value = value;
            MusicChanged.Execute(_musicSlider.value);
        }
    }
}
