using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThreats : MonoBehaviour {
    public GameObject[] threats;
    public GameObject containerPrefab;
    private int counter = 0;
    void Start() {
        StartCoroutine(spawnThreat());
    }

    IEnumerator spawnThreat() {
        int timeToSpawn = Random.Range(0, 3);
        yield return new WaitForSeconds(timeToSpawn);
        counter++;


        int randomXPosition = Random.Range(-200, 200);
        if (counter % 7 == 0)
            Instantiate(containerPrefab, new Vector3(randomXPosition, this.transform.position.y, this.transform.position.z), containerPrefab.transform.rotation);
        else {
            int randomThreatPrefabNum = Random.Range(0, threats.Length);
            Instantiate(threats[randomThreatPrefabNum], new Vector3(randomXPosition, this.transform.position.y, this.transform.position.z), threats[randomThreatPrefabNum].transform.rotation);
        }
        StartCoroutine(spawnThreat());
    }
}
