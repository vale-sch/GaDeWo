using System.Collections;
using UnityEngine;

public enum ShipType { JÄGER, FRACHTER, ALL }
public enum PlatformType { PARK, TRANSFER, LANDING }
public class HangarManager : MonoBehaviour {
    public GameObject[] shipPrefabs; //Jäger - 0; Frachter - 1
    //private Ship[] grid;
    private void Start() {
        InitializeShips();
    }

    private void InitializeShips() {
        foreach (Transform child in transform) {
            ShipPlatform shipPlatformScript = child.GetComponent<ShipPlatform>();
            GameObject ship = Instantiate(shipPrefabs[(int)shipPlatformScript.shipType], shipPlatformScript.dockPoint.position, shipPlatformScript.dockPoint.rotation);
            shipPlatformScript.ParkShip(ship);
        }
    }
}
