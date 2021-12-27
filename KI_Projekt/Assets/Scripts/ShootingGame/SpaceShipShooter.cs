using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooter : MonoBehaviour {
    public float minX;
    public float maxX;
    public float movingSpeed;

    void Update() {
        CheckInput();
    }
    void CheckInput() {
        if (Input.GetKey(KeyCode.D))
            if (transform.root.position.x < maxX) transform.root.transform.Translate(Vector3.right * movingSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.A))
            if (transform.root.position.x > minX) transform.root.transform.Translate(Vector3.left * movingSpeed * Time.deltaTime, Space.World);

    }
}
