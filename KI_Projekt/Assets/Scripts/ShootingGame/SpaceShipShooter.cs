using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooter : MonoBehaviour {
    public float minX;
    public float maxX;
    public float movingSpeed;

    public GameObject bulletPrefab;
    public float coolDown;
    private float startCoolDown;
    private bool hasShoot;
    // Update is called once per frame
    void Awake() {
        startCoolDown = coolDown;
    }
    void Update() {
        CheckInput();
        if (hasShoot) {
            if (coolDown >= 0f)
                coolDown -= Time.deltaTime;
            if (coolDown <= 0f) {
                hasShoot = false;
                coolDown = startCoolDown;
            }

        }
    }
    void CheckInput() {
        if (Input.GetKey(KeyCode.D)) {
            if (transform.root.position.x < maxX) transform.root.transform.Translate(Vector3.right * movingSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A)) {
            if (transform.root.position.x > minX) transform.root.transform.Translate(Vector3.left * movingSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.Space) && !hasShoot) {
            hasShoot = true;
            Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z), bulletPrefab.transform.rotation);
        }
    }
}
