using System.Collections.Generic;
using UnityEngine;

public class ShipsService : BaseService
{
    [SerializeField] private ShipsSpawner _shipSpawner;

    private List<ShipPresenter> _shipPresenters;

    public IReadOnlyList<ShipPresenter> ShipPresenters => _shipPresenters;

    public override void Initialize()
    {
        _shipPresenters = new List<ShipPresenter>();
        _shipPresenters = _shipSpawner.Spawn();
    }
}
