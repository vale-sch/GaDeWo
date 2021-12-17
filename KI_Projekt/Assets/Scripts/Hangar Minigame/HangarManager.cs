using System.Collections;
using UnityEngine;

public enum ShipType { JÄGER, FRACHTER, ALL }
public enum PlatformType { PARK, TRANSFER, LANDING }

public class HangarManager : MonoBehaviour {
    public static HangarManager instance;
    public GameManager gameManager;
    public GameObject[] shipPrefabs; //Jäger - 0; Frachter - 1
    public ShipPlatform transferShipPlatform;
    public ShipPlatform landingShipPlatform;
    private Ship[] shipsInHangar;
    [HideInInspector] public Ship[] shipsInSpace;
    public Transform landingPoint;
    private LandingPlatform landingPlatformScript;
    private bool sceneIsSaved = false;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one HangarManager in scene!");
            return;
        }
        instance = this;
    }
    private void Start() {
        landingPlatformScript = landingPoint.transform.parent.GetComponent<LandingPlatform>();
        if (GameData.hangarIsInitialized) {
            LoadSceneStatus();
            return;
        }
        InitializeShips();
    }

    private void Update() {
        if (landingPlatformScript.ship == null) CheckShipArrival();
    }

    private void CheckShipArrival() {
        for (int i = 0; i < shipsInSpace.Length; i++) {
            if (shipsInSpace[i] != null) {
                if (Time.time > shipsInSpace[i].timeStamp) {
                    ProcessShipArrival(i);
                    return;
                }
            }
        }
    }

    private void ProcessShipArrival(int shipNumber) {
        GameObject returningShip = Instantiate(shipPrefabs[(int)shipsInSpace[shipNumber].shipType], landingPoint.position, landingPoint.rotation);
        returningShip.GetComponent<ShipDrag>().isInSpace = true;
        returningShip.GetComponent<Flight>().isLanding = true;
        landingPlatformScript.ProcessShipArrival(returningShip);
        shipsInSpace[shipNumber] = null;
    }

    public void SaveSpaceShip(GameObject _ship) {
        for (int i = 0; i < shipsInSpace.Length; i++) {
            if (shipsInSpace[i] == null) {
                ShipDrag shipDragScript = _ship.GetComponent<ShipDrag>();
                shipsInSpace[i] = new Ship(shipDragScript.shipType, _ship.transform.position, _ship.transform.rotation, shipDragScript.isInSpace, shipDragScript.timeStamp);
                return;
            }
        }
    }

    private void InitializeShips() {
        shipsInSpace = new Ship[transform.childCount];
        foreach (Transform child in transform) {
            ShipPlatform shipPlatformScript = child.GetComponent<ShipPlatform>();
            GameObject ship = Instantiate(shipPrefabs[(int)shipPlatformScript.shipType], shipPlatformScript.dockPoint.position, shipPlatformScript.dockPoint.rotation);
            shipPlatformScript.ParkShip(ship);
        }
    }

    public void PrepareSceneChange() {
        SaveSceneStatus();
        StartCoroutine(ChangeScene());
    }

    private void SaveSceneStatus() {
        shipsInHangar = new Ship[transform.childCount];
        foreach (Transform child in transform) {
            GameObject ship = child.GetComponent<ShipPlatform>().ship;
            if (ship == null) shipsInHangar[child.GetSiblingIndex()] = null;
            else {
                ShipDrag shipDragScript = ship.GetComponent<ShipDrag>();
                ShipType shipType = shipDragScript.shipType;
                Vector3 position = ship.transform.position;
                Quaternion rotation = ship.transform.rotation;
                bool isInSpace = shipDragScript.isInSpace;
                float timeStamp = shipDragScript.timeStamp;
                shipsInHangar[child.GetSiblingIndex()] = new Ship(shipType, position, rotation, isInSpace, timeStamp);
            }
        }
        
        GameData.shipsInHangar = shipsInHangar;
        GameData.shipsInSpace = shipsInSpace;
        GameData.hangarIsInitialized = true;
        sceneIsSaved = true;
    }

    private IEnumerator ChangeScene() {
        yield return new WaitUntil(() => sceneIsSaved);
        gameManager.OnClick();
    }

    private void LoadSceneStatus() {
        shipsInSpace = GameData.shipsInSpace;
        int platformIndex = 0;
        foreach (Transform child in transform) {
            ShipPlatform shipPlatformScript = child.GetComponent<ShipPlatform>();
            Ship shipIndex = GameData.shipsInHangar[platformIndex];
            if (shipIndex != null) {
                GameObject newShip = Instantiate(shipPrefabs[(int)shipIndex.shipType], shipIndex.position, shipIndex.rotation);
                shipPlatformScript.ParkShip(newShip);
            }
            platformIndex++;
        }
    }
}
