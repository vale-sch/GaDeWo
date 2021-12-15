using UnityEngine.EventSystems;
using UnityEngine;

public class CargoContainer : MonoBehaviour {
    public ContainerSize size;
    public Department department;
    public int supplies;
    public bool isOnTriggerEnter = false;
    public bool hasTriggered = false;
    public bool isRevealed = false;
    public AudioSource containerSound;
    private ContainerUI ui;

    private void Awake() {
        ui = GameObject.Find("ContainerUI").GetComponent<ContainerUI>();
    }

    private void OnTriggerEnter(Collider collider) {
        if (!isOnTriggerEnter) return;
        if (collider.GetComponent<CargoContainer>() || collider.GetComponent<CargoNode>() || collider.GetComponent<TransferNode>()) {
            hasTriggered = true;
            if(!containerSound.isPlaying)containerSound.Play();
        }
    }

    private void OnMouseDown() {
        if (isRevealed) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        ui.ToggleTarget(this);
    }

    public void SetColor() {
        ui.SetTarget(this);
        ui.ChangeColor();
    }
}
