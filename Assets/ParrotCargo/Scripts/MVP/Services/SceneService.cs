using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneService : BaseService
{
    public ReactiveCommand ReloadScene = new ReactiveCommand();

    public override void Initialize()
    {

    }

    public async void ReloadGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene");

        while (loadOperation.progress < 0.9)
            await Task.Delay(1);

        var startGameButtonView = FindObjectOfType<StartGameButtonView>();
        startGameButtonView.GetComponent<Button>().onClick.Invoke();
    }
}
