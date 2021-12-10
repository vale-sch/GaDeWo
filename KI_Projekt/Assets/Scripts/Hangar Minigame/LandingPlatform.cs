using System.Collections;
using UnityEngine;

public class LandingPlatform : MonoBehaviour {
    public Transform dockPoint;
    [HideInInspector] public GameObject ship;

    public void PrepareFlight(GameObject _ship) {
        if (_ship.GetComponent<ShipDrag>().parkingSpot != null) _ship.GetComponent<ShipDrag>().parkingSpot.GetComponent<ParkingSpot>().LeaveParkingSpot();
        _ship.GetComponent<ShipDrag>().parkingSpot = gameObject;
        this.ship = _ship;
        StartCoroutine(FlyShip());
    }

    private IEnumerator FlyShip() {
        yield return new WaitForSeconds(3);
        ship.GetComponent<Flight>().isFlight = true;
    }
}
