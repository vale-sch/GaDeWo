using System.Collections;
using UnityEngine;


public enum ContainerSize { SMALL, MEDIUM, LARGE }
public class CargoManager : MonoBehaviour {
    public GameObject[] containerPrefabs;
    public GameManager gameManager;
    private bool sceneIsSaved = false;
    private Container[][] grid;

    private void Awake() {
        if (GameData.cargoIsInitialized) LoadSceneStatus();
    }

    public void PrepareSceneChange() {
        SaveSceneStatus();
        StartCoroutine(ChangeScene());
    }

    private void LoadSceneStatus() {
        int nodeIndex = 0;
        foreach (Container[] node in GameData.containerGrid) {
            foreach (Container container in node) {
                GameObject newContainer = Instantiate(containerPrefabs[(int)container.size], container.position, container.rotation);
                newContainer.transform.SetParent(transform.GetChild(nodeIndex));
                newContainer.GetComponent<CargoContainer>().department = container.department;
                newContainer.GetComponent<CargoContainer>().isRevealed = container.isRevealed;
                if (container.isRevealed) newContainer.GetComponent<CargoContainer>().SetColor();
            }
            nodeIndex++;
        }
    }

    private void SaveSceneStatus() {
        grid = new Container[transform.childCount][];
        foreach (Transform child in transform) {
            grid[child.GetSiblingIndex()] = new Container[child.childCount];
            foreach (Transform childChild in child) {
                CargoContainer containerScript = childChild.GetComponent<CargoContainer>();
                ContainerSize size = containerScript.size;
                Department department = containerScript.department;
                bool isRevealed = containerScript.isRevealed;
                Vector3 position = childChild.position;
                Quaternion rotation = childChild.rotation;
                grid[child.GetSiblingIndex()][childChild.GetSiblingIndex()] = new Container(size, department, isRevealed, position, rotation);
            }
        }
        GameData.containerGrid = grid;
        GameData.cargoIsInitialized = true;
        sceneIsSaved = true;
    }

    private IEnumerator ChangeScene() {
        yield return new WaitUntil(() => sceneIsSaved);
        gameManager.OnClick();
    }
}
