using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class GameWinView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<PanelAnimationView> _panelAnimationViews;
    [SerializeField] private GameObject _titleAllLevelsCompleted;

    [Header("UI")]
    [SerializeField] private Button _openMenu;
    [SerializeField] private Button _nextLevel;

    private bool _levelProgressSaved;

    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private LevelsService _levelsService;

    public void SetActive(bool isActive)
    {
        if (isActive && gameObject.activeSelf)
            return;

        HandlerPanelAnimation(isActive);

        if (isActive)
        {
            var isNextLevel = _levelsService.TryNextLevel();
            _nextLevel.gameObject.SetActive(isNextLevel);
            _titleAllLevelsCompleted.gameObject.SetActive(isNextLevel == false);

            if (_levelProgressSaved == false)
            {
                _levelsService.SaveProgressLevels();
                _levelProgressSaved = true;
            }
        }
        else
        {
            _levelProgressSaved = false;
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
            SceneService.Instance.ReloadGame(TypeGame.LevelsTypeGame);
        });
    }

    private void OnDestroy()
    {
        _openMenu.onClick.RemoveAllListeners();
        _nextLevel.onClick.RemoveAllListeners();
    }
}
