using Assets.ParrotCargo.Scripts.MVP.Models.Data;
using Assets.ParrotCargo.Scripts.MVP.Views;
using YG;
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
            _view.SoundChanged.Subscribe(value => { _model.SetSound(value); });
            _view.MusicChanged.Subscribe(value => { _model.SetMusic(value); });

            _model.SoundChanged.Subscribe(value => { _view.SetSound(value); });
            _model.MusicChanged.Subscribe(value => { _view.SetMusic(value); });

            _model.AllChanged();
        }
    }
}
