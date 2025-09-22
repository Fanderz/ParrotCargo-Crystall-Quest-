using UniRx;
using UnityEngine;

public class BaseCrystallBagView : MonoBehaviour
{
    private bool _isPicked;
    private Vector3 _localScale;

    public ReactiveCommand<bool> Picked = new ReactiveCommand<bool>();
    public ReactiveCommand Releasing = new ReactiveCommand();

    public bool IsPicked => _isPicked;

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
        if (transform.localScale != _localScale)
            transform.localScale = _localScale;
    }

    public void ChangePicked(bool value)
    {
        _isPicked = value;

        if (_isPicked)
            Picked.Execute(_isPicked);
    }

    public void Release()
    {
        Releasing.Execute();
        Releasing = new();
        Picked = new();
    }
}
