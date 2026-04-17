using System.Collections.Generic;

using UnityEngine;

using UniRx;

public abstract class BaseCrystallBagView : MonoBehaviour
{
    private bool _isPicked;
    private bool _isReleased;
    private Vector3 _defaultLocalScale;
    private Animator _animator;

    [SerializeField] private Material _materialCrystall;
    [SerializeField] private List<MeshRenderer> _crystalls; 

    public ReactiveCommand<bool> Picked = new ReactiveCommand<bool>();
    public ReactiveCommand Releasing = new ReactiveCommand();

    public abstract TypeCrystallBag BagType { get; } 

    public bool IsPicked => _isPicked;
    public bool IsReleased => _isReleased;

    private void Awake()
    {
        _defaultLocalScale = transform.localScale;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach(var crystall in _crystalls)
            crystall.material = _materialCrystall;
    }

    private void OnDisable()
    {
        _isReleased = true;
    }

    private void OnEnable()
    {
        ReturnScale();
        _isReleased = false;
        _animator.SetTrigger("Spawning");
    }

    public void RaiseOnRaycast()
    {
        if (transform.localScale == _defaultLocalScale)
            SetLocalScale(_defaultLocalScale * 1.2f);
    }

    public void ReturnScale()
    {
        if (transform.localScale != _defaultLocalScale)
            SetLocalScale(_defaultLocalScale);
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

    private void SetLocalScale(Vector3 value) =>
        transform.localScale = value;
}
