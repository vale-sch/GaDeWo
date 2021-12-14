using UnityEngine;

public class ShipDrag : MonoBehaviour {
    public ShipType shipType;
    [HideInInspector] public GameObject shipPlatform;
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

        if (hitObject.GetComponent<ShipPlatform>() && hitObject.GetComponent<ShipPlatform>().ship == null && AssertShipType(hitObject.GetComponent<ShipPlatform>().shipType)) {
            ShipPlatform shipPlatformScript = hitObject.GetComponent<ShipPlatform>();

            transform.position = shipPlatformScript.dockPoint.position;
            transform.rotation = shipPlatformScript.dockPoint.rotation;

            shipPlatformScript.ParkShip(gameObject);

            switch (shipPlatformScript.platformType) {
                case PlatformType.PARK:
                    break;
                case PlatformType.TRANSFER:
                    // Transfer Behaviour
                    break;

                case PlatformType.LANDING:
                    hitObject.GetComponent<LandingPlatform>().PrepareFlight(gameObject);
                    break;
            }
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

    private bool AssertShipType(ShipType _shipType) {
        if (_shipType == shipType || _shipType == ShipType.ALL) return true;
        return false;
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
