using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private int _countBag;
    [SerializeField] private int _countBag—ollected;

    public int NumberLevel => _numberLevel;
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
