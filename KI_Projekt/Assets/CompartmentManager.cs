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
            Debug.Log("HALO");
            spaceShipShell.SetActive(false);
            spaceShipInnerrooms.SetActive(true);
            isDissolved = true;
        } else if (cameraObject.transform.position.y > 30 && isDissolved) {
            Debug.Log("Peter");
            spaceShipShell.SetActive(true);
            spaceShipInnerrooms.SetActive(false);
            isDissolved = false;
        }
    }
}
