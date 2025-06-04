using System.Collections.Generic;
using UnityEngine;

public class PalletsSpawner : BaseSpawner<PalletView>
{
    //_xIncrement = 5f;

    public List<PalletPresenter> Spawn()
    {
        List<PalletPresenter> palletPresenters = new List<PalletPresenter>();

        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            var pallet = new Pallet();
            var palletView = SpawnObject(Parent);
            palletView.transform.position = new Vector3(SpawnPoints[0].position.x + _xOffset, SpawnPoints[0].position.y, SpawnPoints[0].position.z);
            var palletPresenter = new PalletPresenter(palletView, pallet);

            palletPresenters.Add(palletPresenter);

            IncreaseOffset(ref _xOffset, IncrementX);
        }

        return palletPresenters;
    }
}
