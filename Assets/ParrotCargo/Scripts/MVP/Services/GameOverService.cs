using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class GameOverService : BaseService
{
    [SerializeField] private Button _buttonRealodGame;

    [Inject] private SceneService _sceneService;

    public override void Initialize()
    {
        _buttonRealodGame.onClick.AddListener(() =>
        {
            _sceneService.ReloadGame();
        });
    }

    private void OnDestroy()
    {
        _buttonRealodGame.onClick.RemoveAllListeners();
    }
}
