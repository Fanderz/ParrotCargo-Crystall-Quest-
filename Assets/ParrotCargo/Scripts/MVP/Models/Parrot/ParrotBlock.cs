using UnityEngine;
using System.Collections.Generic;

public class ParrotBlock
{
    private Transform _transform;
    private List<Parrot> _parrots;

    public ParrotBlock(Transform transform)
    {
        _parrots = new List<Parrot>();
        _transform = transform;
    }

    public bool IsChoosed { get; private set; }

    public void ChooseParrotBlock()
    {
        IsChoosed = true;
    }

    public void MoveParrots(Vector3 newPosition)
    {
        _transform.position = newPosition;
    }

    public void Picking()
    {
        foreach (Parrot parrot in _parrots)
        {
            //parrot.PickBag();
        }
    }
}
