using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSprite : MonoBehaviour {
    void Start() {
        StartCoroutine(DestroyMySelf());
    }
    void Update() {
        this.transform.Translate(-Vector3.up * 0.25f, Space.World);

    }
    IEnumerator DestroyMySelf() {
        yield return new WaitForSeconds(10f);
        if (this.gameObject)
            Destroy(this.gameObject);
    }
}
