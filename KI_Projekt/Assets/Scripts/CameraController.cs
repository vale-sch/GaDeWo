using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollspeed = 5f;
    public float minX = -40f;
    public float maxX = 100f;
    public float minY = 5f;
    public float maxY = 70f;
    public float minZ = -40f;
    public float maxZ = 50f;


    private void Update() {

        CheckInput();
    }

    private void CheckInput() {
        //Input.GetKey("w") ||
        if (Input.mousePosition.y >= Screen.height - panBorderThickness) {
            if (transform.position.z < maxZ) transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        //Input.GetKey("s") ||
        if (Input.mousePosition.y <= panBorderThickness / 3) {
            if (transform.position.z > minZ) transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        //Input.GetKey("d") ||
        if (Input.mousePosition.x >= Screen.width - panBorderThickness) {
            if (transform.position.x < maxX) transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        //Input.GetKey("a") ||
        if (Input.mousePosition.x <= panBorderThickness) {
            if (transform.position.x > minX) transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollspeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
