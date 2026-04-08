using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    [SerializeField] private GameStarterService _gameStarter;   

    private static SceneService _instance;

    public static SceneService Instance => _instance;

    public async void ReloadGame(TypeGame typeGame)
    {
        Debug.Log("Вызвался SceneService.ReloadGame");
        Debug.Log("<size=50>Вызвался SceneService.ReloadGame</size>");

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (loadOperation.progress < 0.9)
        {
            await Task.Delay(1);
            Debug.Log("Загрузка сцены...");
            Debug.Log("<size=50>Загрузка сцены...</size>");
        }

        Debug.Log("Сцена загрузилась.");
        Debug.Log("<size=50>Сцена загрузилась.</size>");

        GameOverService.Instance.InvokeReloadGame(typeGame);
        //_gameStarter.StartGame();
    }

    public async void RestartScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (loadOperation.progress < 0.9)
            await Task.Delay(1);
    }

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

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
