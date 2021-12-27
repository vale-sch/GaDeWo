using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunCamera : MonoBehaviour {
    public GameObject shootParticlePrefab;
    public float coolDown;
    private float startCoolDown;
    private bool hasShoot;
    void Awake() {
        startCoolDown = coolDown;

    }
    void Update() {
        CheckInput();
        if (hasShoot) {
            if (coolDown >= 0f)
                coolDown -= Time.deltaTime;
            else {
                hasShoot = false;
                coolDown = startCoolDown;
            }
        }
    }

    void CheckInput() {
        this.transform.rotation = Quaternion.Euler(Input.mousePosition.x, 90, -90);
        if (Input.GetKey(KeyCode.Space) && !hasShoot) {
            hasShoot = true;
            var bulletChache = Instantiate(shootParticlePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(Input.mousePosition.x - 90, 90, 0));
            bulletChache.GetComponent<BulletScript>().flyDir = Quaternion.Euler(Input.mousePosition.x - 90, 90, 0) * Vector3.forward;
            Debug.Log(bulletChache.GetComponent<BulletScript>().flyDir);
        }
    }
}
