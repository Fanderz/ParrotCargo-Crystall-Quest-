using System;
using UnityEngine;

using UniRx;

public class Parrot
{
    private bool HaveBag;
    private BaseCrystallBag _pickedBag;
    private Vector3 _position;

    public ReactiveCommand<Vector3> UpdatedPositionCommand = new ReactiveCommand<Vector3>();


    public void PickBag(BaseCrystallBag pickedBag)
    {
        if (pickedBag == null)
            throw new ArgumentNullException();

        _pickedBag = pickedBag;
        HaveBag = true;
    }

    public void SetPosition(Vector3 newPosition)
    {
        _position = newPosition;
        UpdatedPositionCommand.Execute(_position);
    }

    public void PutBag()
    {
        if (_pickedBag == null)
            throw new NullReferenceException();

        HaveBag = false;
    }
}
