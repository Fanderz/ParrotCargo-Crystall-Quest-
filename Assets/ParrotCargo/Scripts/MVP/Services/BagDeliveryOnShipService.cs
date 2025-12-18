using System.Collections.Generic;

using UnityEngine;

public class BagDeliveryOnShipService : MonoBehaviour
{
    private List<ParrotBlockPresenter> _parrotPresenters;
    private List<ShipPresenter> _shipPresenters;

    public void Initialize(List<ParrotBlockPresenter> parrotPresenters, List<ShipPresenter> shipPresenters)
    {
        _parrotPresenters = parrotPresenters;
        _shipPresenters = shipPresenters;
    }
}
