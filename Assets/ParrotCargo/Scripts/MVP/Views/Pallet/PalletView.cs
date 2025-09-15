using UniRx;
using UnityEngine;

public class PalletView : MonoBehaviour
{
    [SerializeField] private float _bagTargetOffsetY = 6;

    private BaseCrystallBagView _crystallBag;

    public bool HaveBag { get; private set; }
    public bool HaveCourier { get; private set; }

    public ReactiveCommand<bool> EmptyChanged = new ReactiveCommand<bool>();

    public Vector3 BagTargetPosition => new Vector3(transform.position.x, transform.position.y + _bagTargetOffsetY, transform.position.z);

    public void TakeBag(BaseCrystallBagView crystallBag)
    {
        _crystallBag = crystallBag;
        //_crystallBag.transform.SetParent(transform);
        //_crystallBag.transform.position = new Vector3(transform.position.x, transform.position.y + _bagTargetOffsetY, transform.position.z);
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

    private void ChangeEmpty(bool value)
    {
        HaveBag = value;
        EmptyChanged.Execute(HaveBag);
    }
}
