using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PointsModel : BaseScoreModel
{
    public PointsModel(int inputValue)
    {
        value = inputValue;
    }
}
