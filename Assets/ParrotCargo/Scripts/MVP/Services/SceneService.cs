using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    private static SceneService _instance;

    public static SceneService Instance => _instance;

    public void ReloadGame()
    {
        RestartScene();

        GameOverService.Instance.InvokeReloadGame();
    }

    public async void RestartScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (loadOperation.progress < 0.9)
            await Task.Delay(1);
    }

    public async void SetTimeScale(float value)
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
