using System.Collections.Generic;
using UnityEngine;

public class ParrotBlock
{
    private List<Parrot> _parrots;

    public ParrotBlock()
    {
        _parrots = new List<Parrot>();
    }

    public bool IsChoosed { get; private set; }

    public void ChooseParrotBlock()
    {
        IsChoosed = true;
    }

    public void MoveParrots(Vector3 position)
    {
        foreach(Parrot parrot in _parrots)
        {
            parrot.SetPosition(position);
        }
    }

    public void Picking()
    {
        foreach (Parrot parrot in _parrots)
        {
            //parrot.PickBag();
        }
    }
}
