using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpawner : MonoBehaviour {
    public GameObject[] containerPrefab;
    public Transform[] spawnPoints;
    void Start() {
        StartCoroutine(spawnContainers());
    }

    IEnumerator spawnContainers() {
        float randomTime = Random.Range(4f, 8f);

        yield return new WaitForSeconds(randomTime);
        int randomNumSpawnPoint = Random.Range(0, 5);
        int randomNumPrefab = Random.Range(0, 5);

        GameObject instContainer = Instantiate(containerPrefab[randomNumPrefab], spawnPoints[randomNumSpawnPoint].position, Quaternion.identity);
        instContainer.name = "Container" + GameData.containersInGame;
        StartCoroutine(SaveContainer(instContainer, randomNumPrefab));

        StartCoroutine(spawnContainers());
    }

    IEnumerator SaveContainer(GameObject container, int prefabNumber) {
        yield return new WaitForSeconds(3);
        GameData.SaveContainer(prefabNumber, container.transform.position, container.transform.rotation, false, container.name);
    }

}
