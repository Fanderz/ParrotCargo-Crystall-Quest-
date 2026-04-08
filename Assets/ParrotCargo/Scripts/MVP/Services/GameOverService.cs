using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class GameOverService : BaseService
{
    [SerializeField] private Button _buttonReloadGame;
    [SerializeField] private Button _buttonStartGame;

    [Inject] private LevelsService _levelsService;
    [Inject] private TypeGameService _typeGameService;
    [Inject] private GameStarterService _gameStarterService;

    private static GameOverService _instance;

    public static GameOverService Instance => _instance;

    public override void Initialize()
    {
        _buttonReloadGame.onClick.AddListener(() =>
        {
            SceneService.Instance.ReloadGame(_typeGameService.CurrentTypeGame);
        });
    }

    public void InvokeReloadGame(TypeGame typeGame)
    {
        Debug.Log("<size=50>Вызвался GameOverService.InvokeReloadGame</size>");
        ////Time.timeScale = 1f;

        if (typeGame == TypeGame.LevelsTypeGame)
        {
            _typeGameService.SetTypeGame(TypeGame.LevelsTypeGame);
            _levelsService.ReloadCurrentLevel();
        }
 
        _gameStarterService.StartGame();
    }

    private void Awake()
    {
        Debug.Log("GameOverService Awake");
        _instance = this;
    }

    private void OnDestroy()
    {
        _buttonReloadGame.onClick.RemoveAllListeners();
    }
}
