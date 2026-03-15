using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color _colorNoActive;
    [SerializeField] private Level _level;
    [Header("UI")]
    [SerializeField] private Image _iconLevel;
    [SerializeField] private Button _buttonLevel;

    public void Initialize(int curentNumberLevel)
    {
        var isActive = _level.NumberLevel <= curentNumberLevel;

        if (isActive == false)
            _iconLevel.color = _colorNoActive;

        _buttonLevel.enabled = isActive;
    }

    public void SubscribeButtonClick(Action<Level> action)
    {
        _buttonLevel.onClick.AddListener(() => { action?.Invoke(_level); });
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
