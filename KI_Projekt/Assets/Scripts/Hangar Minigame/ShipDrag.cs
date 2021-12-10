using UnityEngine;

public class ShipDrag : MonoBehaviour {
    public ShipType shipType;
    [HideInInspector] public GameObject parkingSpot;
    private Vector3 offset;
    private float mouseZPos;
    private Vector3 oldShipPosition;

    private void OnMouseDown() {
        oldShipPosition = transform.position;
        mouseZPos = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp() {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        Physics.Raycast(downRay, out hit);
        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.GetComponent<ParkingSpot>() && hitObject.GetComponent<ParkingSpot>().shipType == shipType) {
            transform.position = hitObject.GetComponent<ParkingSpot>().dockPoint.position;
            transform.rotation = hitObject.GetComponent<ParkingSpot>().dockPoint.rotation;
            hitObject.GetComponent<ParkingSpot>().ParkShip(gameObject);
            return;
        }
        if (hitObject.GetComponent<TransferSpot>() && shipType == ShipType.FRACHTER) {
            transform.position = hitObject.GetComponent<TransferSpot>().dockPoint.position;
            transform.rotation = hitObject.GetComponent<TransferSpot>().dockPoint.rotation;
            parkingSpot.GetComponent<ParkingSpot>().LeaveParkingSpot();
            return;
        }
        if (hitObject.GetComponent<LandingPlatform>()) {
            transform.position = hitObject.GetComponent<LandingPlatform>().dockPoint.position;
            transform.rotation = hitObject.GetComponent<LandingPlatform>().dockPoint.rotation;
            hitObject.GetComponent<LandingPlatform>().PrepareFlight(gameObject);
            return;
        }
        transform.position = oldShipPosition;
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZPos;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDrag() {
        if (AssertPosition()) transform.position = GetMouseWorldPosition() + offset;
    }

    private bool AssertPosition() {
        float minZ = -24;
        float maxZ = 24;
        float minX = -29;
        float maxX = 29;
        if (transform.position.z > maxZ || transform.position.z < minZ || transform.position.x > maxX || transform.position.x < minX) return false;
        return true;
    }
}
