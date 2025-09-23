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
}
