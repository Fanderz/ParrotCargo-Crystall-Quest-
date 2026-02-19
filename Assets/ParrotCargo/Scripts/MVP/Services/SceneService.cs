using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    private static SceneService _instance;

    public static SceneService Instance => _instance;

    public async void ReloadGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (loadOperation.progress < 0.9)
            await Task.Delay(1);

        GameOverService.Instance.InvokeReloadGame();
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
