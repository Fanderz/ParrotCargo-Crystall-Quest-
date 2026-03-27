using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : BaseLangTextSO
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private int _countBag;
    [SerializeField] private int _countBag—ollected;
    [SerializeField] private TypeCrystallBag _bagType;

    public int NumberLevel => _numberLevel;
    public TypeCrystallBag BagType => _bagType;
    public int CountBag => _countBag;
    public int —ountBag—ollected => _countBag—ollected;

    public void Add—ountBag—ollected()
    {
        if (_countBag—ollected < CountBag)
            ++_countBag—ollected;
    }

    public bool TryFinishLevel()
        => _countBag—ollected == CountBag;
}
