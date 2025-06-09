using System;
using UnityEngine;
using UniRx;

public class Parrot
{
    private bool _haveBag;
    private BaseCrystallBag _pickedBag;

    public bool CanPick { get; private set; }

    public void PickBag(BaseCrystallBag pickedBag)
    {
        if (pickedBag == null)
            throw new ArgumentNullException();

        _pickedBag = pickedBag;
        _haveBag = true;
    }

    public void PutBag()
    {
        if (_pickedBag == null)
            throw new NullReferenceException();

        _haveBag = false;
    }

    public void SetPickable(bool value)
    {
        CanPick = value;
    }
}
