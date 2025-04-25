public class BaseCrystallBag
{
    public bool IsPicked { get; private set; }

    public void SetPicked()
    {
        IsPicked = true;
    }

    public bool GetEqual(Ship ship)
    {
        if (ship == null)
            return false;

        return true;
    }
}
