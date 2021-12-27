using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    // Start is called before the first frame update
    [HideInInspector]
    public Vector3 flyDir;
    void Start() {
        StartCoroutine(DestroyMySelf());
    }

    // Update is called once per frame
    void Update() {
        this.transform.Translate(flyDir, Space.World);
    }
    IEnumerator DestroyMySelf() {
        yield return new WaitForSeconds(2.5f);
        if (this.transform)
            Destroy(this.gameObject);
    }
}
