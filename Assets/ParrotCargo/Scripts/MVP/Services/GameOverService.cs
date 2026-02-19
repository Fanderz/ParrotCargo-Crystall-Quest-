using UnityEngine;
using UnityEngine.UI;

using UniRx;
using Cysharp.Threading.Tasks;

public class GameOverService : BaseService
{
    [SerializeField] private Button _buttonReloadGame;
    [SerializeField] private Button _buttonStartGame;

    private static GameOverService _instance;

    public static GameOverService Instance => _instance;

    public override void Initialize()
    {
        _buttonReloadGame.onClick.AddListener(() =>
        {
            SceneService.Instance.ReloadGame();
        });
    }

    public async void InvokeReloadGame()
    {
        Time.timeScale = 1f;
        _buttonStartGame.onClick.Invoke();
    }

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _buttonReloadGame.onClick.RemoveAllListeners();
    }
}
