using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStopPoint : MonoBehaviour
{
    public bool isEmpty { get; private set; }

    private void Awake()
    {
        isEmpty = true;
    }

    public void ChangeEmpty(bool value)
    {
        isEmpty = value;
    }
}
