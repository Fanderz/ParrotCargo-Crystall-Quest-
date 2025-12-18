using Assets.ParrotCargo.Scripts.MVP.Views;
using Assets.ParrotCargo.Scripts.MVP.Models.Data;

using UniRx;

namespace Assets.ParrotCargo.Scripts.MVP.Presenters
{
    public class SettingsPresenter
    {
        private SettingsView _view;
        private SettingsModel _model;

        public SettingsPresenter(SettingsModel model, SettingsView view)
        {
            _model = model;
            _view = view;

            Initialize();
        }

        public void Initialize()
        {
            _view.Initialize(_model);

            _view.SoundChanged.Subscribe(value => { _model.SetSound(value); });
            _view.MusicChanged.Subscribe(value => { _model.SetMusic(value); });
        }
    }
}
