using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private int _countBag;
    [SerializeField] private int _countBagÑollected;

    public int NumberLevel => _numberLevel;
    public int CountBag => _countBag;
    public int ÑountBagÑollected => _countBagÑollected;
}
