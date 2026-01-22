using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class GameOverService : BaseService
{
    [SerializeField] private Button _buttonRealodGame;
    [SerializeField] private Button _buttonReloadGame;

    private static GameOverService _instance;

    public static GameOverService Instance => _instance;

    public override void Initialize()
    {
        _buttonRealodGame.onClick.AddListener(() =>
        {
            SceneService.Instance.ReloadGame();
        });
    }

    public void InvokeReloadGame()
        => _buttonReloadGame.onClick.Invoke();

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _buttonRealodGame.onClick.RemoveAllListeners();
    }
}
