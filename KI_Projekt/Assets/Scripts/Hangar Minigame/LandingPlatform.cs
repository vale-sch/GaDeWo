using System.Collections;
using UnityEngine;

public class LandingPlatform : MonoBehaviour {
    public Transform landingPoint;
    [HideInInspector] public GameObject[] shipsInSpace;
    private void Start() {
        shipsInSpace = new GameObject[10];
    }

    public void PrepareFlight(GameObject _ship) {
        SendShipToSpace(_ship);
        StartCoroutine(FlyShip(_ship));
    }

    private void SendShipToSpace(GameObject _ship) {
        for (int i = 0; i < shipsInSpace.Length; i++) {
            if (shipsInSpace[i] == null) {
                shipsInSpace[i] = _ship;
                return;
            }
        }
    }

    private IEnumerator FlyShip(GameObject _ship) {
        yield return new WaitForSeconds(3);
        _ship.GetComponent<Flight>().isFlying = true;
    }
}
