public class BaseCrystallBag
{
    public bool IsPicked { get; private set; }

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
