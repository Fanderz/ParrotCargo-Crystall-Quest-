using UnityEngine;

using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using Assets.ParrotCargo.Scripts.MVP.Presenters;
using Assets.ParrotCargo.Scripts.MVP.Views;

using YG;
using UniRx;
using Zenject;

public class SettingsService : BaseService
{
    [SerializeField] private SettingsView _settingsView;

    private bool _isChanged;

    private SettingsModel _settingsModel;
    private SettingsPresenter _settingsPresenter;

    [Inject] private AudioService _audioService;

    public override void Initialize()
    {
        _settingsModel = YG2.saves.playerSettings;

        _settingsPresenter = new SettingsPresenter(_settingsModel, _settingsView);

        _settingsView.SoundChanged.Subscribe(volume => { _audioService.SetEffectsVolume(volume); _isChanged = true; });
        _settingsView.MusicChanged.Subscribe(volume => { _audioService.SetMusicVolume(volume); _isChanged = true; });
    }

    //public void SaveSettings()
    //{
    //    if (_isChanged)
    //    {
    //        YG2.SaveProgress();
    //        _isChanged = false;
    //    }
    //}
}
