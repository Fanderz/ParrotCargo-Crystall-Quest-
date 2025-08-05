using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    private List<Pallet> _pallets;

    public Ship(Vector3 pointOnFilled)
    {
        _pallets = new List<Pallet>();
        TargetOnFilled = pointOnFilled;
    }

    public Vector3 TargetOnFilled { get; private set; }
    public bool isGoingToRelease { get; private set; }

    public void AddPallet(Pallet pallet)
    {
        _pallets.Add(pallet);
    }

    public void SetGoingToRelease(bool value)
    {
        isGoingToRelease = value;
    }
}
