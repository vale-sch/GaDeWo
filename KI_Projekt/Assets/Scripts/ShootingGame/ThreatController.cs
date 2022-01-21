using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreatController : MonoBehaviour {
    private GameObject gameOverText;

    public AudioSource destroySound;

    void Start() {
        StartCoroutine(DestroyMySelf());
        gameOverText = GameObject.Find("Menu").transform.GetChild(2).gameObject;
    }
    void Update() {
        this.transform.Translate(-Vector3.up * 0.25f, Space.World);

    }
    IEnumerator DestroyMySelf() {
        yield return new WaitForSeconds(10f);
        if (this.gameObject)
            Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider spaceObject) {
        if (spaceObject.GetComponent<BulletScript>())
            if (this.gameObject) {
                StartCoroutine(playDestroySound(spaceObject.gameObject));
            }
        if (spaceObject.GetComponent<SpaceShipShooter>())
            if (this.gameObject) {
                Destroy(spaceObject.transform.root.gameObject);
                gameOverText.SetActive(!gameOverText.activeSelf);
                this.GetComponent<AudioSource>().Play();
            }
    }
    IEnumerator playDestroySound(GameObject spaceObject) {
        destroySound.Play();
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(spaceObject);
        while (destroySound.isPlaying) {
            yield return null;
        }
        Destroy(this.gameObject);

    }
}

