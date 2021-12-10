using System.Collections;
using UnityEngine;

public enum ShipType { JÄGER, FRACHTER }
public class HangarManager : MonoBehaviour {
    public GameObject[] shipPrefabs; //Jäger - 0; Frachter - 1
    //private Ship[] grid;
    private void Start() {
        InitializeShips();
    }

    private void InitializeShips() {
        foreach (Transform child in transform) {
            ParkingSpot parkSpotScript = child.GetComponent<ParkingSpot>();
            GameObject ship = Instantiate(shipPrefabs[(int)parkSpotScript.shipType], parkSpotScript.dockPoint.position, parkSpotScript.dockPoint.rotation);
            parkSpotScript.ParkShip(ship);
        }
    }
}
