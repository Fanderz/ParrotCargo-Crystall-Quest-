using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine.Accessibility;

public class MainSettingsUIView : MonoBehaviour
{
    [SerializeField] private Button _buttonSettingsOpen;
    [SerializeField] private Button _buttonSettingsClose;
    [SerializeField] private Button _buttonGoHome;
    [SerializeField] private Button _buttonBackToGame;
    [SerializeField] private PanelAnimationView _panelAnimationView;
    [SerializeField] private GameObject _startingUiView;

    private bool _isInGame;

    [Inject] private PlayerProgressService _playerProgressService;

    public async void ChangeActive()
    {
        var isActive = gameObject.activeSelf;

        if (isActive == false)
        {
            ChangeActiveButtons(isActive);
            gameObject.SetActive(true);
            _panelAnimationView.Show();
            await UniTask.Delay(1000);
            _playerProgressService.SetTimeScale(0);
        }
        else
        {
            _playerProgressService.SetTimeScale(1);
            _panelAnimationView.Hide();
            await UniTask.Delay(700);
            gameObject.SetActive(false);
            ChangeActiveButtons(isActive);
        }
    }

    public void SetGameState(bool isInGame)
    {
        _isInGame = isInGame;
    }

    private void ChangeActiveButtons(bool isActive)
    {
        _buttonGoHome.gameObject.SetActive(_isInGame);
        _buttonSettingsOpen.gameObject.SetActive(isActive);

        if (_isInGame == false)
            _startingUiView.SetActive(isActive);
    }
}
