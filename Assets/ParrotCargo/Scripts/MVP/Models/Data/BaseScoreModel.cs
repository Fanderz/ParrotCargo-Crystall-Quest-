using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using YG;

public class BaseScoreModel
{
    protected int currentValue;
    public int Value { get { return currentValue; } set { currentValue = value; } }

    public ReactiveCommand<int> ValueChanged = new ReactiveCommand<int>();

    public void Initialize()
    {
        ValueChanged.Execute(currentValue);
    }

    public void ChangeValue(int inputValue)
    {
        if (currentValue != inputValue)
        {
            currentValue = inputValue;
            ValueChanged.Execute(currentValue);
        }
    }

}
