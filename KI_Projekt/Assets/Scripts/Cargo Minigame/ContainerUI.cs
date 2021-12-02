using UnityEngine;

public class ContainerUI : MonoBehaviour {
    public GameObject ui;
    private CargoContainer target;
    public Color[] depColors;
    public static bool uiActive;

    private void Update() {
        uiActive = ui.activeSelf;
    }
    public void ToggleTarget(CargoContainer _target) {
        this.target = _target;
        transform.position = target.transform.position;
        ui.SetActive(!ui.activeSelf);
    }

    public void RevealContents() {
        ChangeColor();
        target.isRevealed = true;
        this.target = null;
        ui.SetActive(false);
    }

    public void SetTarget(CargoContainer _target) {
        this.target = _target;
    }

    public void ChangeColor() {
        int depNumber = (int)target.GetComponent<CargoContainer>().department - 1;

        foreach (Transform child in target.transform.GetChild(0).transform) {
            child.GetComponent<MeshRenderer>().material.color = depColors[depNumber];
        }
    }
}
