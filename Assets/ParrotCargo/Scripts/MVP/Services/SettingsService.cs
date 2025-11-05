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

    private SettingsPresenter _settingsPresenter;

    public override void Initialize()
    {
        _settingsPresenter = new SettingsPresenter(YG2.saves.playerSettings, _settingsView);
    }
}
