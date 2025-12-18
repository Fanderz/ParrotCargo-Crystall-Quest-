using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSettings", menuName = "ScriptableObject/DefaultSettings")]
public class DefaultSettings : ScriptableObject
{
    [Header("Звук")]
    [SerializeField] private float _soundValue;
    [SerializeField] private float _musicValue;

    public float SoundValue => _soundValue;
    public float MusicValue => _musicValue;

    public void FirstLoadingSetts(DefaultSettings defaultSettings)
    {
        _soundValue = defaultSettings.SoundValue;
        _musicValue = defaultSettings.MusicValue;
    }
}
