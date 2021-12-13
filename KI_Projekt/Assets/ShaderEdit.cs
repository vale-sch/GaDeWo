using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEdit : MonoBehaviour {

    public Material dissMat;
    private bool gettingUp = true;
    private bool gettingDown = false;

    private float value = 0.26f;
    void Update() {

        if (value <= 0.3f && gettingUp) {
            value += Time.fixedDeltaTime * 0.0025f;
            dissMat.SetFloat("_Dissolve", value);
            if (value >= 0.295f) {
                gettingDown = true;
                gettingUp = false;
            }
        }
        if (value >= 0.1f && gettingDown) {
            value -= Time.fixedDeltaTime * 0.0025f;
            dissMat.SetFloat("_Dissolve", value);
            if (value <= 0.11f) {
                gettingDown = false;
                gettingUp = true;
            }
        }
    }
}
