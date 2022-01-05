using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunCamera : MonoBehaviour {
    public GameObject shootParticlePrefab;
    public string weaponType;
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
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (!hasShoot) {
                hasShoot = true;
                if (weaponType == "flak") {
                    var bulletChache = Instantiate(shootParticlePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(Input.mousePosition.x - 90, 90, 0));
                    bulletChache.GetComponent<BulletScript>().flyDir = Quaternion.Euler(Input.mousePosition.x - 90, 90, 0) * Vector3.forward;
                }
                if (weaponType == "rail") {
                    this.transform.GetChild(this.transform.childCount - 1).GetComponent<Lightbug.LaserMachine.LaserMachine>().m_inspectorProperties.m_maxRadialDistance += 120;
                }

            }
        }

    }
}
