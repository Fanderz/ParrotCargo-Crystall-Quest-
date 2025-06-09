using System;
using UnityEngine;
using UniRx;

public class Parrot
{
    private bool _haveBag;
    private bool _canPickBag;

    private BaseCrystallBag _pickedBag;

    public bool CanPick => _canPickBag;

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
        _canPickBag = value;
    }
}
