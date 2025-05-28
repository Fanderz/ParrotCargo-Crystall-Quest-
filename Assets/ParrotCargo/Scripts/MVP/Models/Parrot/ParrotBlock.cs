using System.Collections.Generic;
using UnityEngine;

public class ParrotBlock
{
    private RectTransform _rectTransform;
    private List<Parrot> _parrots;

    public ParrotBlock(RectTransform rect)
    {
        _parrots = new List<Parrot>();
        _rectTransform = rect;
    }

    public bool IsChoosed { get; private set; }

    public void ChooseParrotBlock()
    {
        IsChoosed = true;
    }

    public void MoveParrots(Vector2 newPosition)
    {
        _rectTransform.anchoredPosition = newPosition;

        //foreach(Parrot parrot in _parrots)
        //{
        //    parrot.SetPosition(position);
        //}
    }

    public void Picking()
    {
        foreach (Parrot parrot in _parrots)
        {
            //parrot.PickBag();
        }
    }
}
