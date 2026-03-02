using UnityEngine;

using TMPro;

public class LevelProgressView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _numverLevelView;
    [SerializeField] private TextMeshProUGUI _countBagÑollectedView;

    public void SetActive(bool isActive)
        => gameObject.SetActive(isActive);

    public void UpdateNumverLevelView(Level level)
    {
        _numverLevelView.text = "Óğîâåíü " + level.NumberLevel;
    }

    public void UpdateCountBagÑollectedView(Level level)
    {
        _countBagÑollectedView.text = "Ñîáğàíî ìåøî÷êîâ: " + level.ÑountBagÑollected + "/" + level.CountBag;
    }
}
