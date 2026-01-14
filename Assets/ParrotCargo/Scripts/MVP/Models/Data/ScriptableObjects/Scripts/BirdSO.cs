using UnityEngine;

[CreateAssetMenu(fileName = "BirdSO", menuName = "ScriptableObject/BirdSO")]
public class BirdSO : BaseShopObjectSO
{
    [SerializeField] private TypeBird _typeBird;
    [SerializeField] private GameObject _prefabBird;

    public TypeBird TypeBird => _typeBird;
    public GameObject PrefabBird => _prefabBird;
}
