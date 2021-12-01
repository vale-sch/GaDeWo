using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class VolumeRealtimeEdit : MonoBehaviour {


    public VolumeProfile volumeProfile;
    public CafeCameraController cafeCameraController;
    public SpawnWalkers spawnWalkers;
    private Vignette vignette;
    void Awake() {
        if (volumeProfile.TryGet<Vignette>(out vignette)) {
            vignette.intensity.value = 1;
            vignette.smoothness.value = 1;
        }
    }

    void Update() {

        if (vignette.intensity.value > 0.6f) {
            vignette.intensity.value -= Time.fixedDeltaTime * 0.045f;
            vignette.smoothness.value -= Time.fixedDeltaTime * 0.035f;
        } else {
            cafeCameraController.enabled = true;
            spawnWalkers.enabled = true;
            this.enabled = false;
        }
    }
}
