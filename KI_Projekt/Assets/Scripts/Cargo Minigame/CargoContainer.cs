using UnityEngine.EventSystems;
using UnityEngine;

public class CargoContainer : MonoBehaviour {
    public ContainerSize size;
    public Department department;
    public int supplies;
    public bool isOnTriggerEnter = false;
    public bool hasTriggered = false;
    public bool isRevealed = false;
    private ContainerUI ui;

    private void Awake() {
        ui = GameObject.Find("ContainerUI").GetComponent<ContainerUI>();
    }

    private void OnTriggerEnter(Collider collider) {
        if (!isOnTriggerEnter) return;
        if (collider.tag == "Container" || collider.tag == "CargoNode" || collider.tag == "TransferNode") hasTriggered = true;
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
