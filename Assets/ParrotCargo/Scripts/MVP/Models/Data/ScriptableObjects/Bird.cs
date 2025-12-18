using UnityEngine;

[CreateAssetMenu(fileName = "Bird", menuName = "ScriptableObject/Bird")]
public class Bird : ScriptableObject
{
    [SerializeField] private TypeBird _typeBird;
    [SerializeField] private GameObject _prefabBird;

    public TypeBird TypeBird => _typeBird;
    public GameObject PrefabBird => _prefabBird;
}
