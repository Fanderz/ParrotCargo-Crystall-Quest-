using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;

public class TypeGameUIView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<PanelAnimationView> _panelAnimationViews;
    [SerializeField] private LevelsView _levelsView;
    [Header("UI")]
    [SerializeField] private Button _playEndlessGame;
    [SerializeField] private Button _playLevelsGame;

    public async void SetActive(bool isActive)
    {
        HandlerPanelAnimation(isActive);

        if (isActive == false)
            await UniTask.Delay(1000);

        gameObject.SetActive(isActive);
    }

    private void Start()
    {
        _playEndlessGame.onClick.AddListener(() => { SetActive(false); });
        _playLevelsGame.onClick.AddListener(() => { _levelsView.SetActive(true); });
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

    private void OnDestroy()
    {
        _playEndlessGame.onClick.RemoveAllListeners();
        _playLevelsGame.onClick.RemoveAllListeners();
    }
}
