using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : BaseLangTextSO
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private int _countBag;
    [SerializeField] private int _countBagCollected;
    [SerializeField] private TypeCrystallBag _bagType;

    public int NumberLevel => _numberLevel;
    public TypeCrystallBag BagType => _bagType;
    public int CountBag => _countBag;
    public int CountBagCollected => _countBagCollected;

    public void AddCountBagCollected()
    {
        if (_countBagCollected < CountBag)
            ++_countBagCollected;
    }

    public bool TryFinishLevel()
        => _countBagCollected == CountBag;

    public void ResetCollectedBags()
        => _countBagCollected = 0;
}