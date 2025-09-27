using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSettings", menuName = "ScriptableObject/DefaultSettings")]
public class DefaultSettings : ScriptableObject
{
    [Header("«вук")]
    [SerializeField] private float _soundValue;
    [SerializeField] private float _musicValue;
    [Space]
    [Header("язык")]
    [SerializeField] private LanguageSettingsEnum _languageSetting;

    public float SoundValue => _soundValue;
    public float MusicValue => _musicValue;
    public LanguageSettingsEnum LanguageValue => _languageSetting;

    public void FirstLoadingSetts(DefaultSettings defaultSettings)
    {
        _soundValue = defaultSettings.SoundValue;
        _musicValue = defaultSettings.MusicValue;
        _languageSetting = defaultSettings.LanguageValue;
    }
}
