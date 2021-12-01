using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeCameraController : MonoBehaviour {
    void Update() {
        float mouseY = Input.GetAxis("Mouse Y");
        float rotX = this.transform.rotation.eulerAngles.x;
        if (mouseY > 0 && rotX > 5 || mouseY < 0 && rotX < 20)
            transform.Rotate(new Vector3(-mouseY * 0.75f, 0, 0));

        float mouseX = Input.GetAxis("Mouse X");
        float rotY = this.transform.rotation.eulerAngles.y;
        if (mouseX > 0 && rotY < 180 || mouseX < 0 && rotY > 100)
            transform.Rotate(new Vector3(0, mouseX * 0.75f, 0));
    }
}
