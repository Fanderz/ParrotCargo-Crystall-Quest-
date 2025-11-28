using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using Assets.ParrotCargo.Scripts.MVP.Presenters;
using Assets.ParrotCargo.Scripts.MVP.Views;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using YG;


public class SettingsService : BaseService
{
    [SerializeField] private SettingsView _settingsView;

    private SettingsModel _settingsModel;
    private SettingsPresenter _settingsPresenter;
    public override void Initialize()
    {
        _settingsModel = YG2.saves.playerSettings;

        _settingsPresenter = new SettingsPresenter(_settingsModel, _settingsView);
    }

    public void SetSettings()
    {
        YG2.saves.playerSettings.Sound = _settingsModel.Sound;
        YG2.saves.playerSettings.Music = _settingsModel.Music;
    }

    public void SaveSettings()
    {
        SetSettings();
        YG2.SaveProgress();
    }
}
