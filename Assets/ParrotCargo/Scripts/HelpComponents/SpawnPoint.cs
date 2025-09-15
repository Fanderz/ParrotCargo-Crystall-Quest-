using UniRx;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool haveBirds { get; private set; }

    public ReactiveCommand OnSpawnPointEmpty = new ReactiveCommand();

    public void GiveAwayBirds()
    {
        ChangeBusyness(false);

        OnSpawnPointEmpty.Execute();
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
