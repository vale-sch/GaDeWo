using UnityEngine;

public class ShipDrag : MonoBehaviour {
    public ShipType shipType;
    public bool isInSpace = false;
    public float timeStamp;
    private LandingPlatform landingPlatformScript;
    [HideInInspector] public GameObject shipPlatform;
    private Vector3 offset;
    private float mouseZPos;
    private Vector3 oldShipPosition;
    private bool shipIsTakingOff = false;
    private HangarUI hangarUI;

    private void Start() {
        landingPlatformScript = GameObject.Find("LandingPlatform").GetComponent<LandingPlatform>();
        hangarUI = HangarUI.instance;
    }

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

        if (isInSpace) {
            if (hitObject.GetComponent<ShipPlatform>() && hitObject.GetComponent<ShipPlatform>().ship == null && AssertShipType(hitObject.GetComponent<ShipPlatform>().shipType)) {
                ShipPlatform shipPlatformScript = hitObject.GetComponent<ShipPlatform>();
                if (shipType == ShipType.FRACHTER && shipPlatformScript.platformType == PlatformType.TRANSFER) {
                    isInSpace = false;
                    StartCoroutine(landingPlatformScript.LeaveLandingPlatform());
                    SetToPlatform(shipPlatformScript);
                    GameData.containersInTransfer++;
                    hangarUI.DisplayText(hangarUI.transferText);
                    return;
                }
                if (shipType == ShipType.JÄGER && shipPlatformScript.platformType == PlatformType.PARK) {
                    isInSpace = false;
                    StartCoroutine(landingPlatformScript.LeaveLandingPlatform());
                    SetToPlatform(shipPlatformScript);
                    return;
                }
            }
            hangarUI.DisplayText(hangarUI.spaceErrorText);
            transform.position = oldShipPosition;
            return;
        }

        if (hitObject.GetComponent<ShipPlatform>() && hitObject.GetComponent<ShipPlatform>().ship == null && AssertShipType(hitObject.GetComponent<ShipPlatform>().shipType)) {
            ShipPlatform shipPlatformScript = hitObject.GetComponent<ShipPlatform>();

            SetToPlatform(shipPlatformScript);

            if (shipPlatformScript.platformType == PlatformType.LANDING) {
                hitObject.GetComponent<LandingPlatform>().PrepareFlight(gameObject);
                shipIsTakingOff = true;
            }
            return;
        }
        transform.position = oldShipPosition;
    }

    private void SetToPlatform(ShipPlatform shipPlatformScript) {
        transform.position = shipPlatformScript.dockPoint.position;
        transform.rotation = shipPlatformScript.dockPoint.rotation;
        shipPlatformScript.ParkShip(gameObject);
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZPos;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDrag() {
        if (shipIsTakingOff) return;
        if (AssertPosition()) transform.position = GetMouseWorldPosition() + offset;
    }

    private bool AssertShipType(ShipType _shipType) {
        if (_shipType == shipType || _shipType == ShipType.ALL) return true;
        return false;
    }

    private bool AssertPosition() {
        float minZ = -29;
        float maxZ = 29;
        float minX = -59;
        float maxX = 59;
        if (transform.position.z > maxZ || transform.position.z < minZ || transform.position.x > maxX || transform.position.x < minX) return false;
        return true;
    }
}
