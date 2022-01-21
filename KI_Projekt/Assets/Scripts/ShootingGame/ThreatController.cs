using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatController : MonoBehaviour {
    private GameObject gameOverText;

    public AudioSource destroySound;
    public AudioSource gameOverSound;

    void Start() {
        gameOverText = GameObject.Find("Menu").transform.GetChild(2).gameObject;
    }
    void Update() {
        this.transform.Translate(-Vector3.up * 0.25f, Space.World);
    }

    void OnTriggerEnter(Collider spaceObject) {
        if (spaceObject.GetComponent<BulletScript>())
            StartCoroutine(playDestroySound(spaceObject.gameObject));
        if (spaceObject.GetComponent<SpaceShipShooter>()) {
            gameOverSound.Play();
            Destroy(spaceObject.transform.root.gameObject);
            gameOverText.SetActive(!gameOverText.activeSelf);
        }
        if (spaceObject.GetComponent<DestroyThreats>())
            StartCoroutine(DestroyMySelf());
    }
    IEnumerator DestroyMySelf() {
        while (destroySound.isPlaying)
            yield return null;
        Destroy(this.gameObject);
    }
    IEnumerator playDestroySound(GameObject spaceObject) {
        if (this.gameObject) {
            destroySound.Play();
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            Destroy(spaceObject);
            while (destroySound.isPlaying) {
                yield return null;
            }
            Destroy(this.gameObject);
        }
    }
}

