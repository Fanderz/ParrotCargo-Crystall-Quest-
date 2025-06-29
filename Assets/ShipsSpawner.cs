using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsSpawner : BaseSpawner<BaseShipView>
{
    [SerializeField] private List<Transform> _targetPoints;

    public List<ShipPresenter> Spawn()
    {
        List<ShipPresenter> ShipPresenters = new List<ShipPresenter>();

        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            var ship = new Ship();
            var shipView = SpawnObject(Parent);
            shipView.Initialize(_targetPoints[i], SpawnPoints[i].position);
            var shipPresenter = new ShipPresenter(shipView, ship);

            ShipPresenters.Add(shipPresenter);
        }

        return ShipPresenters;
    }

    private void FixedUpdate()
    {
        
    }
}
