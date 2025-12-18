using System;

[Serializable]
public class CoinsModel : BaseScoreModel
{
    public CoinsModel(int inputValue)
    {
        currentValue = inputValue;
    }
}
