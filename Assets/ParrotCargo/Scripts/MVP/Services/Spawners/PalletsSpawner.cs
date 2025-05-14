using System.Collections.Generic;
using UnityEngine;

public class PalletsSpawner : BaseSpawner<PalletView>
{
    public List<PalletPresenter> Spawn(Transform parent)
    {
        List<PalletPresenter> palletPresenters = new List<PalletPresenter>();

        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            var pallet = new Pallet();
            var palletView = SpawnObject(parent);
            var palletPresenter = new PalletPresenter(palletView, pallet);

            palletPresenters.Add(palletPresenter);
        }

        return palletPresenters;
    }
}
