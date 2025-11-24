using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class PointsModel : BaseScoreModel
{
    public PointsModel(int inputValue)
    {
        currentValue = inputValue;
    }
}
