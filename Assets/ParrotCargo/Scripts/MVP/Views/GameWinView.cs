using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;
using Zenject;

public class GameWinView : MonoBehaviour
{
    [SerializeField] private List<PanelAnimationView> _panelAnimationViews;
    [SerializeField] private Button _openMenu;
    [SerializeField] private Button _nextLevel;

    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private LevelsService _levelsService;

    public async void SetActive(bool isActive)
    {
        HandlerPanelAnimation(isActive);

        if (isActive == false)
            await UniTask.Delay(1000);

        if (isActive)
        {
            if (_levelsService.TryNextLevel() == false)
                _nextLevel.gameObject.SetActive(false);
        }

        gameObject.SetActive(isActive);
    }

    private void HandlerPanelAnimation(bool isActive)
    {
        foreach (var panelAnimationView in _panelAnimationViews)
        {
            if (isActive)
                panelAnimationView.Show();
            else
                panelAnimationView.Hide();
        }
    }

    private void Start()
    {
        _openMenu.onClick.AddListener(() =>
        {
            _playerProgressService.SaveProgress();
            _playerProgressService.ResetProgress();
        });

        _nextLevel.onClick.AddListener(() =>
        {
            _playerProgressService.SaveProgress();
            _levelsService.NextLevel();
        });
    }

    private void OnDestroy()
    {
        _openMenu.onClick.RemoveAllListeners();
        _nextLevel.onClick.RemoveAllListeners();
    }
}
