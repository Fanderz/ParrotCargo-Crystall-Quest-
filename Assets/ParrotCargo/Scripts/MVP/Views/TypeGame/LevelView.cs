using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color _colorNoActive;
    [SerializeField] private Level _level;
    [Header("UI")]
    [SerializeField] private Image _iconLevel;
    [SerializeField] private Button _buttonLevel;

    public ReactiveCommand<int> CurrentLevelClicked = new();

    public void Initialize(int curentNumberLevel)
    {
        var isActive = _level.NumberLevel <= curentNumberLevel;

        if (isActive == false)
            _iconLevel.color = _colorNoActive;

        _buttonLevel.enabled = isActive;
    }

    public void SubscribeButtonClick(Action<Level> action)
    {
        _buttonLevel.onClick.AddListener(() => { action?.Invoke(_level); CurrentLevelClicked.Execute(_level.NumberLevel); });
    }

    private void OnValidate()
    {
        if (_iconLevel == null)
            _iconLevel = GetComponent<Image>();

        if (_buttonLevel == null)
            _buttonLevel = GetComponent<Button>();
    }
    private void OnDestroy()
    {
        _buttonLevel.onClick.RemoveAllListeners();
    }
}
