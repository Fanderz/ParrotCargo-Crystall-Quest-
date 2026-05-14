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

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _buttonReloadGame.onClick.RemoveAllListeners();
    }

    public override void Initialize()
    {
        _buttonReloadGame.onClick.AddListener(() =>
        {
            SceneService.Instance.ReloadGame(_typeGameService.CurrentTypeGame);
        });
    }

    public void InvokeReloadGame(TypeGame typeGame)
    {
        if (typeGame == TypeGame.LevelsTypeGame)
        {
            _typeGameService.SetTypeGame(TypeGame.LevelsTypeGame);
            _levelsService.ReloadCurrentLevel();
        }

        Debug.Log($"[GameOverService.InvokeReloadGame] TimeScale: {Time.timeScale}");
        _gameStarterService.StartGame();
    }
}
