using UnityEngine;
using System.Collections.Generic;
using UniRx;

public class ParrotBlock
{
    private readonly Transform _transform;
    private readonly List<Parrot> _parrots;

    public ReactiveCommand<bool> Pickable = new ReactiveCommand<bool>();

    public ParrotBlock(Transform transform)
    {
        _parrots = new List<Parrot>();
        _transform = transform;
    }

    public bool IsMoving { get; private set; }
    public bool CanPickBags { get; private set; }

    public void AddParrot(Parrot parrot)
    {
        _parrots.Add(parrot);
    }

    public void MoveParrots(Vector3 newPosition)
    {
        _transform.position = newPosition;
    }

    public void SetPickable(bool value)
    {
        CanPickBags = value;

        Pickable.Execute(CanPickBags);
    }

    public void ChangeMovable(bool value)
    {
        IsMoving = value;
    }
}
