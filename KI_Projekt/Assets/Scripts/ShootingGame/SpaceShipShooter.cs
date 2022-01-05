using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooter : MonoBehaviour {
    public float minX;
    public float maxX;
    public float movingSpeed;
    public Transform extendGunPos;
    private Vector3 rootGunsPos;
    public GameObject flak;
    public GameObject railGun;
    void Awake() {
        rootGunsPos = flak.transform.position;
    }
    void Update() {
        CheckInput();
    }
    void CheckInput() {
        if (Input.GetKey(KeyCode.D))
            if (transform.root.position.x < maxX) transform.root.transform.Translate(Vector3.right * movingSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.A))
            if (transform.root.position.x > minX) transform.root.transform.Translate(Vector3.left * movingSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Q))
            StartCoroutine(ActivateGun(flak));
        if (Input.GetKey(KeyCode.W))
            StartCoroutine(ActivateGun(railGun));

    }
    IEnumerator ActivateGun(GameObject gun) {
        float dis = Vector3.Distance(gun.transform.position, extendGunPos.position);
        while (dis > 0.1f) {
            gun.transform.position = Vector3.Lerp(gun.transform.position, extendGunPos.position, Time.deltaTime * 0.05f);
            dis = Vector3.Distance(gun.transform.position, extendGunPos.position);
            yield return null;
        }
        gun.transform.GetChild(gun.transform.childCount - 1).GetComponent<RotateGunCamera>().enabled = true;
    }
    IEnumerator DisableFlak(GameObject gun) {
        gun.transform.GetChild(gun.transform.childCount - 1).GetComponent<RotateGunCamera>().enabled = false;
        float dis = Vector3.Distance(gun.transform.position, rootGunsPos);
        while (dis > 0.1f) {
            gun.transform.position = Vector3.Lerp(gun.transform.position, rootGunsPos, Time.deltaTime * 0.05f);
            dis = Vector3.Distance(gun.transform.position, rootGunsPos);
            yield return null;
        }
    }
}
