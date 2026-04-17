using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

using Cysharp.Threading.Tasks;

public class SceneService : MonoBehaviour
{
    [SerializeField] private GameStarterService _gameStarter;

    private static SceneService _instance;

    public static SceneService Instance => _instance;

    public async void ReloadGame(TypeGame typeGame)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (!loadOperation.isDone)
            await UniTask.Delay(200, delayType: DelayType.UnscaledDeltaTime);

        GameOverService.Instance.InvokeReloadGame(typeGame);
    }

    public async void RestartScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (!loadOperation.isDone)
            await Task.Delay(100);
    }

    //public void SetTimeScale(float value)
    //{
    //    Time.timeScale = value;
    //    Debug.Log($"[SceneService.SetTimeScale] TimeScale: {Time.timeScale}");
    //}

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);
    }
}
