using UnityEngine;

public class ParkingSpot : MonoBehaviour {
    public ShipType shipType;
    public Transform dockPoint;
    [HideInInspector] public GameObject ship;

    public void SetAsParkingSpot(GameObject _ship) {
        if (_ship.GetComponent<ShipDrag>().parkingSpot != null) _ship.GetComponent<ShipDrag>().parkingSpot.GetComponent<ParkingSpot>().LeaveParkingSpot();
        _ship.GetComponent<ShipDrag>().parkingSpot = gameObject;
        this.ship = _ship;
    }

    public void LeaveParkingSpot() {
        ship = null;
    }
}
