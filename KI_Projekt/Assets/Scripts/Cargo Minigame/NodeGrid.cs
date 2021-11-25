using UnityEngine;

public class NodeGrid : MonoBehaviour {
    public GameObject[] containerPrefabs;

    private void Awake() {
        if (GameData.containers.Count > 0) LoadGameStatus();
    }

    private void LoadGameStatus() {
        SpawnContainers();
        SetNodeStatus();
    }

    private void SpawnContainers() {
        foreach (Container container in GameData.containers) {
            GameObject newContainer = Instantiate(containerPrefabs[container.prefabNumber], container.position, container.rotation);
            newContainer.name = container.name;
            newContainer.GetComponentInChildren<GridControlContainer>().hasContainer = container.hasContainer;
        }
    }

    private void SetNodeStatus() {
        for (int i = 0; i < transform.childCount - 5; i++) {
            transform.GetChild(i).GetComponent<GridControl>().hasContainer = GameData.nodeHasContainer[i];
        }
    }
}
