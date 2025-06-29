using System;
using UnityEngine;
using UniRx;

public class Parrot
{
    private bool _haveBag;

    public void PickBag(bool pickedBag)
    {
        _haveBag = pickedBag;
    }

    public void PutBag()
    {
        if (_haveBag)
            _haveBag = false;
    } 
}
