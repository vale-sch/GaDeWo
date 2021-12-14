using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlatform : MonoBehaviour {
    public PlatformType platformType;
    public ShipType shipType;
    public Transform dockPoint;
    [HideInInspector] public GameObject ship;

    public void ParkShip(GameObject _ship) {
        if (_ship.GetComponent<ShipDrag>().shipPlatform != null) _ship.GetComponent<ShipDrag>().shipPlatform.GetComponent<ShipPlatform>().LeaveParkingSpot();
        _ship.GetComponent<ShipDrag>().shipPlatform = gameObject;
        this.ship = _ship;
    }

    public void LeaveParkingSpot() {
        ship = null;
    }
}
