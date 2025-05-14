using System.Collections.Generic;
using UnityEngine;

public class PalletService : BaseService
{
    [SerializeField] private PalletsSpawner _palletSpawner;

    private List<PalletPresenter> _palletPresenters;

    public override void Initialize()
    {
        _palletPresenters = new List<PalletPresenter>();
        _palletPresenters = _palletSpawner.Spawn(Parent);
    }
}
