using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool haveBirds { get; private set; }

    public void GiveAwayBirds()
    {
        ChangeBusyness(false);
    }

    public void GetBirds()
    {
        ChangeBusyness(true);
    }

    private void ChangeBusyness(bool value)
    {
        haveBirds = value;
    }
}
