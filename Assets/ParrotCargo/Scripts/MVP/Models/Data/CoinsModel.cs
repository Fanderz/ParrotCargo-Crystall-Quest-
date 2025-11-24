using System;
using UniRx;
using UnityEngine;

[Serializable]
public class CoinsModel : BaseScoreModel
{
    public CoinsModel(int inputValue)
    {
        currentValue = inputValue;
    }
}
