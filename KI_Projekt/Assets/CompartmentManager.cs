using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompartmentManager : MonoBehaviour {
    public GameObject cameraObject;
    public GameObject spaceShipShell;
    public GameObject spaceShipInnerrooms;
    private bool isDissolved;
    // Start is called before the first frame update
    void Update() {
        if (cameraObject.transform.position.y < 30 && !isDissolved) {
            spaceShipShell.SetActive(!spaceShipShell.activeSelf);
            spaceShipInnerrooms.SetActive(!spaceShipInnerrooms.activeSelf);
            isDissolved = true;
        } else if (cameraObject.transform.position.y > 30 && isDissolved) {
            spaceShipShell.SetActive(!spaceShipShell.activeSelf);
            spaceShipInnerrooms.SetActive(!spaceShipInnerrooms.activeSelf);
            isDissolved = false;
        }
    }
}
