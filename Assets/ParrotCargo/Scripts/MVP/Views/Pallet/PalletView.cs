using UnityEngine;

using UniRx;

public class PalletView : MonoBehaviour
{
    [SerializeField] private float _bagTargetOffsetY = 6;

    [Header("ChangesMaterialsPallet")]
    [SerializeField] private MeshRenderer _palletMesh;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _yellowMaterial;
    [SerializeField] private Material _purpleMaterial;

    private BaseCrystallBagView _crystallBag;

    public bool HaveBag { get; private set; }

    public ReactiveCommand<bool> EmptyChanged = new ReactiveCommand<bool>();

    public Vector3 BagTargetPosition => new Vector3(transform.position.x, transform.position.y + _bagTargetOffsetY, transform.position.z);

    public void OnTakeBag(BaseCrystallBagView crystallBag)
    {
        _crystallBag = crystallBag;
        ChangeEmpty(true);
    }

    public void RemoveBag()
    {
        ChangeEmpty(false);
    }

    public void Clear()
    {
        if (_crystallBag != null && _crystallBag.IsPicked == false)
        {
            _crystallBag.Release();
            RemoveBag();
        }
    }

    public BaseCrystallBagView GetBag()
    {
        return _crystallBag;
    }

    public void ChangeMaterial(TypeCrystallBag typeBag)
    {
            if (typeBag == TypeCrystallBag.BlueBag)
                _palletMesh.materials[0] = _blueMaterial;
            else if (typeBag == TypeCrystallBag.YellowBag)
                _palletMesh.materials[0] = _yellowMaterial;
            else if (typeBag == TypeCrystallBag.PurpleBag)
                _palletMesh.materials[0] = _purpleMaterial;
            else if (typeBag == TypeCrystallBag.GreenBag)
                _palletMesh.materials[0] = _greenMaterial;
    }

    public void ReturnMaterial()
    {
        _palletMesh.materials[0] = _defaultMaterial;
    }

    private void ChangeEmpty(bool value)
    {
        HaveBag = value;
        EmptyChanged.Execute(HaveBag);
    }
}

public class NullablePalletView : PalletView
{

}
