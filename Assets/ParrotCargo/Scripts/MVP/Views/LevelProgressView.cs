using UnityEngine;

using TMPro;

public class LevelProgressView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _numverLevelView;
    [SerializeField] private TextMeshProUGUI _countBagСollectedView;

    [Header("TranslatedCollectedTitle")]
    [SerializeField] private string _ruTitle;
    [SerializeField] private string _enTitle;
    [SerializeField] private string _trTitle;

    public void SetActive(bool isActive)
        => gameObject.SetActive(isActive);

    public void UpdateNumverLevelView(Level level)
    {
        _numverLevelView.text = level.NumberLevel.ToString();
    }

    public void UpdateCountBagСollectedView(Level level)
    {
        _countBagСollectedView.text = $"{level.GetTranslatedText()} {level.СountBagСollected}/{level.CountBag}";
    }
}
