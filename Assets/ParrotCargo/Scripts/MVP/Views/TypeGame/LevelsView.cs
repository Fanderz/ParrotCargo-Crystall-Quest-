using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using YG;
using Cysharp.Threading.Tasks;
using Zenject;

public class LevelsView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PanelAnimationView _panelAnimationView;
    [SerializeField] private List<LevelView> _levelsView;
    [SerializeField] private Button _buttonEndlessTypeGame;

    [Inject] private LevelsService _levelsService;
    [Inject] private TypeGameService _typeGameService;

    public async void SetActive(bool isActive)
    {
        if (isActive)
            _panelAnimationView.Show();
        else
            _panelAnimationView.Hide();

        if(isActive == false)
            await UniTask.Delay(1000);

        if (isActive)
            Initialize();

        gameObject.SetActive(isActive);
    }

    private void Initialize()
    {
        var currentNumberLevel = YG2.saves.currentNumberLevel;

        for (int i = 0; i < _levelsView.Count; i++)
            _levelsView[i].Initialize(i <= currentNumberLevel);
    }

    private void Start()
    {
        for (int i = 0; i < _levelsView.Count; i++)
            _levelsView[i].SubscribeButtonClick(async (level) =>
            {
                SetActive(false);
                await UniTask.Delay(500);
                _buttonEndlessTypeGame.onClick.Invoke();
                _levelsService.Initialize(level);

                //костыль
                _typeGameService.SetTypeGame(TypeGame.LevelsTypeGame);
            });
    }
}
