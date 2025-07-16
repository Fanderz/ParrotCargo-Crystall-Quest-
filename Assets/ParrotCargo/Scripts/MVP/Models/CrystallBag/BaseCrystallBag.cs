using UnityEngine;

public class BaseCrystallBag
{
    public bool IsPicked { get; private set; }
    public Vector3 StartPosition { get; private set; }

    public BaseCrystallBag(Vector3 startPosition)
    {
        StartPosition = startPosition;
    }

    public void SetPicked(bool value)
    {
        IsPicked = value;
    }

    //public bool GetEqual(Ship ship)
    //{
    //    if (ship == null)
    //        return false;

    //    return true;
    //}
}
