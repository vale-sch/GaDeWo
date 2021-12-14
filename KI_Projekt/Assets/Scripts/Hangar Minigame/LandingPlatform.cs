using System.Collections;
using UnityEngine;

public class LandingPlatform : MonoBehaviour {
    public Transform landingPoint;
    [HideInInspector] public GameObject ship;

    public void PrepareFlight(GameObject _ship) {
        StartCoroutine(FlyShip(_ship));
    }
    private IEnumerator FlyShip(GameObject _ship) {
        yield return new WaitForSeconds(3);
        _ship.GetComponent<Flight>().isFlying = true;
    }

    public void ProcessShipArrival(GameObject _ship) {
        this.ship = _ship;
    }

    public IEnumerator LeaveLandingPlatform() {
        yield return new WaitForSeconds(3);
        ship = null;
    }
}
