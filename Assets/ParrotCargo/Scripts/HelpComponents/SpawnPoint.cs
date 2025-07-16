using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool haveBirds { get; private set; }

    public void TakeBirds()
    {
        ChangeBusyness(false);
    }

    public void SetBirds()
    {
        ChangeBusyness(true);
    }

    private void ChangeBusyness(bool value)
    {
        haveBirds = value;
    }
}
