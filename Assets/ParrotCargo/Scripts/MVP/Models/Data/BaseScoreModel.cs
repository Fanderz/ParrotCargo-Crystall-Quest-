using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BaseScoreModel
{
    protected int value;
    public int Value => value;

    public ReactiveCommand<int> ValueChanged = new ReactiveCommand<int>();

    public void ChangeValue(int inputValue)
    {
        if (value != inputValue)
        {
            value = inputValue;
            ValueChanged.Execute(value);
        }
    }
}
