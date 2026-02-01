using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;
using Zenject;

public class MainSettingsUIView : MonoBehaviour
{
    [SerializeField] private Button _buttonSettingsOpen;
    [SerializeField] private Button _buttonSettingsClose;
    [SerializeField] private Button _buttonGoHome;
    [SerializeField] private Button _buttonBackToGame;
    [SerializeField] private PanelAnimationView _panelAnimationView;

    [Inject] private PlayerProgressService _playerProgressService;

    public async void ChangeActive(bool isInGame)
    {
        ChangeActiveButtons(isInGame);
        var isActive = gameObject.activeSelf;

        if (isActive == false)
        {
            gameObject.SetActive(true);
            _panelAnimationView.Show();
            await UniTask.Delay(1000);
            _playerProgressService.SetTimeScale(0);
        }
        else
        {
            _playerProgressService.SetTimeScale(1);
            _panelAnimationView.Hide();
            await UniTask.Delay(1000);
            gameObject.SetActive(false);
        }
    }

    private void ChangeActiveButtons(bool isInGame)
    {
        _buttonSettingsClose.gameObject.SetActive(!isInGame);
        _buttonGoHome.gameObject.SetActive(isInGame);
        _buttonBackToGame.gameObject.SetActive(isInGame);
    }
}
