using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSprite : MonoBehaviour {

    void Update() {
        this.transform.Translate(-Vector3.up * 0.25f, Space.World);
    }

}
