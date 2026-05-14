using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;
using Zenject;
using YG;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;

public class MainSettingsUIView : MonoBehaviour
{
    [SerializeField] private Button _buttonSettingsOpen;
    [SerializeField] private Button _buttonGoHome;
    //[SerializeField] private Button _ruLangButton;
    //[SerializeField] private Button _enLangButton;
    //[SerializeField] private Button _trLangButton;

    [SerializeField] private GameObject _startingUiView;

    private bool _isInGame;
    private PanelAnimationView _panelAnimationView;

    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private PauseService _pauseService;

    private void Awake()
    {
        _panelAnimationView = GetComponent<PanelAnimationView>();
    }

    //private void OnEnable()
    //{
    //    _ruLangButton.onClick.AddListener(() => YG2.SwitchLanguage("ru"));
    //    _enLangButton.onClick.AddListener(() => YG2.SwitchLanguage("en"));
    //    _trLangButton.onClick.AddListener(() => YG2.SwitchLanguage("tr"));
    //}

    //private void OnDisable()
    //{
    //    _ruLangButton.onClick.RemoveAllListeners();
    //    _enLangButton.onClick.RemoveAllListeners();
    //    _trLangButton.onClick.RemoveAllListeners();
    //}

    public async void ChangeActive()
    {
        var isActive = gameObject.activeSelf;

        if (isActive == false)
        {
            _pauseService.SetPausedBySettings(_isInGame);
            ChangeActiveButtons(isActive);
            gameObject.SetActive(true);
            _panelAnimationView.Show();
        }
        else
        {
            _panelAnimationView.Hide();
            ChangeActiveButtons(isActive);
            _pauseService.SetPausedBySettings(false);

            await UniTask.Delay(500);

            gameObject.SetActive(false);
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
        else
            _startingUiView.SetActive(false);
    }
}
