using UnityEngine;

using TMPro;

public class LevelProgressView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _numverLevelView;
    [SerializeField] private TextMeshProUGUI _countBagСollectedView;

    public void UpdateNumverLevelView(Level level)
    {
        _numverLevelView.text = level.NumberLevel.ToString();
    }

    public void UpdateCountBagСollectedView(Level level)
    {
        _countBagСollectedView.text = $"{level.GetTranslatedText()} {level.СountBagСollected}/{level.CountBag}";
    }
}
