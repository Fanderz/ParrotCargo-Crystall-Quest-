using System;

[Serializable]
public class PointsModel : BaseScoreModel
{
    public PointsModel(int inputValue)
    {
        currentValue = inputValue;
    }
}
