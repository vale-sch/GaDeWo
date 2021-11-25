using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatController : MonoBehaviour {
    private GameObject gameOverText;

    void Start() {
        StartCoroutine(DestroyMySelf());
        gameOverText = GameObject.Find("Menu").transform.GetChild(2).gameObject;
    }
    void Update() {
        this.transform.Translate(-Vector3.up * 0.25f, Space.World);

    }
    IEnumerator DestroyMySelf() {
        yield return new WaitForSeconds(4f);
        if (this.gameObject)
            Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider spaceObject) {
        if (spaceObject.GetComponent<BulletScript>())
            if (this.gameObject)
                Destroy(this.gameObject);
        if (spaceObject.GetComponent<SpaceShipShooter>())
            if (this.gameObject) {
                Destroy(spaceObject.transform.root.gameObject);
                gameOverText.SetActive(!gameOverText.activeSelf);
            }
    }
}
