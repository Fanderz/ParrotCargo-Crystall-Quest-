using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[Preserve]
public class SceneService : MonoBehaviour
{
    private static SceneService _instance;

    public static SceneService Instance => _instance;

    public async void ReloadGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (loadOperation.progress < 0.9)
            await UniTask.Delay(1);

        GameOverService.Instance.InvokeReloadGame();
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
