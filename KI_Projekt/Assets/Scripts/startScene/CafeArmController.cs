using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeArmController : MonoBehaviour {
    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            this.transform.Rotate(Vector3.forward * 0.1f);
        }
        if (Input.GetKey(KeyCode.S)) {
            this.transform.Rotate(Vector3.back * 0.1f);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.parent.Rotate(Vector3.left * 0.1f);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.parent.transform.Rotate(Vector3.right * 0.1f);
        }
    }
}
