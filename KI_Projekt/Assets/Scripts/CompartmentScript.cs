using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CompartmentScript : MonoBehaviour {
    public string gameSceneToLoad;
    private Color c;
    private Color cA;
    void Awake() {
        c = this.gameObject.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");

        cA = c * 2.5f;

    }

    void OnMouseDown() {
        SceneManager.LoadScene(gameSceneToLoad);
    }
    void OnMouseOver() {

        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", cA);

    }
    void OnMouseExit() {
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", c);
    }
}
