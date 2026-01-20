using UnityEngine;

using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using Assets.ParrotCargo.Scripts.MVP.Presenters;
using Assets.ParrotCargo.Scripts.MVP.Views;

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

    public void OnSave()
    {
        YG2.saves.playerSettings = _settingsModel;
    }

    public void SaveSettings()
    {
        OnSave();
        YG2.SaveProgress();
    }
}
