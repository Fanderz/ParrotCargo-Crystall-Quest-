using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;

public class ParrotsBlockView : MonoBehaviour
{
    [SerializeField] private float _zOffsetOnPick;
    [SerializeField] private List<ParrotView> _parrots;

    private Vector3 _startPosition;
    private float _zPickingValue;

    public bool EachParrotHaveBag => _parrots.TrueForAll(parrot => parrot.HaveBag == true);
    public IReadOnlyList<ParrotView> Parrots => _parrots;
    private List<IDisposable> _disposables;

    public bool IsMoving { get; private set; }
    public bool CanPickBag { get; private set; }

    public ReactiveCommand<Vector3> BlockMoving = new ReactiveCommand<Vector3>();
    public ReactiveCommand<bool> Movable = new ReactiveCommand<bool>();
    public ReactiveCommand<bool> Activation = new ReactiveCommand<bool>();
    public ReactiveCommand SearchingRecievers = new ReactiveCommand();

    private void Awake()
    {
        _disposables = new List<IDisposable>();
    }

    private void OnEnable()
    {
        Activation.Execute(gameObject.activeSelf);
        GetComponent<DraggableParrotBlock>().enabled = true;
    }

    private void OnDisable()
    {
        IsMoving = false;
        CanPickBag = false;
    }

    public void Initialize()
    {
        //SetActive(true);

        _startPosition = transform.position;
        _zPickingValue = transform.position.z - _zOffsetOnPick / 2;
    }

    //public void SetActive(bool isEnabled)
    //{
    //    gameObject.SetActive(isEnabled);
    //}

    public void MoveBlock(Vector3 newPosition)
    {
        IsMoving = true;
        transform.position = newPosition;

        BlockMoving.Execute(transform.position);
        Movable.Execute(IsMoving);
    }

    public void StopMoveBlock()
    {
        IsMoving = false;

        BlockMoving.Execute(transform.position);
        Movable.Execute(IsMoving);
    }

    public void ScanBags()
    {
        foreach (ParrotView parrot in _parrots)
        {
            if (parrot.gameObject.activeSelf)
                parrot.ScanBag();
        }

        CanPickBag = _parrots.TrueForAll(parrot => parrot.CanPick == true);
    }

    public void PickBags()
    {
        MoveBlock(new Vector3(transform.position.x, transform.position.y, _zPickingValue));
        StopMoveBlock();

        if (CanPickBag)
        {
            foreach (ParrotView parrot in _parrots)
                parrot.PickBag();
        }
    }

    public void ReturnToBase()
    {
        MoveBlock(_startPosition);
        StopMoveBlock();
    }

    //private void OnDrawGizmos()
    //{
    //    float step = 1f;
    //    float radius = 100f;
    //    Vector3 direction = transform.forward;
    //    Vector3 origin = transform.position;

    //    for (float i = 0; i < 30f; i += step)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(origin + direction * i, radius);
    //    }
    //}
}
