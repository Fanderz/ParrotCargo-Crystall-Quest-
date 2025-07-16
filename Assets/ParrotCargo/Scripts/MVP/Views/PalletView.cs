using UniRx;
using UnityEngine;

public class PalletView : MonoBehaviour
{
    [SerializeField] private float _bagTargetOffsetY = 6;

    public bool HaveBag { get; private set; }

    public ReactiveCommand<bool> EmptyChanged = new ReactiveCommand<bool>();

    public Vector3 BagTargetPosition => new Vector3(transform.position.x, transform.position.y + _bagTargetOffsetY, transform.position.z);

    public void TakeBag()
    {
        ChangeEmpty(true);
    }

    public void RemoveBag()
    {
        ChangeEmpty(false);
    }

    private void ChangeEmpty(bool value)
    {
        HaveBag = value;
        EmptyChanged.Execute(HaveBag);
    }
}
