using UnityEngine;

using TMPro;

public class LevelProgressView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _numverLevelView;
    [SerializeField] private TextMeshProUGUI _countBagCollectedView;

    public void UpdateNumverLevelView(Level level)
    {
        _numverLevelView.text = level.NumberLevel.ToString();
    }

    public void UpdateCountBagCollectedView(Level level)
    {
        _countBagCollectedView.text = $"{level.GetTranslatedText()} {level.CountBagCollected}/{level.CountBag}";
    }
}