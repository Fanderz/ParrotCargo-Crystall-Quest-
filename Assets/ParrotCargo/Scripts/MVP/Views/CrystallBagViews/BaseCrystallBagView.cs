using UniRx;
using UnityEngine;

public class BaseCrystallBagView : MonoBehaviour
{
    private bool _isPicked;
    private Vector3 _localScale;

    public ReactiveCommand Raising = new ReactiveCommand();
    public ReactiveCommand<bool> Picked = new ReactiveCommand<bool>();

    private void Awake()
    {
        _localScale = transform.localScale;
    }

    public void RaiseOnRaycast()
    {
        if (transform.localScale == _localScale)
            transform.localScale *= 1.2f;
    }

    public void ReturnScale()
    {
        transform.localScale = _localScale;
    }

    public void ChangePicked(bool value)
    {
        _isPicked = value;
    }
}
