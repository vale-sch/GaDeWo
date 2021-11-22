using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThreats : MonoBehaviour
{
    public GameObject[] threats;
    void Start()
    {
        StartCoroutine(spawnThreat());
    }

    IEnumerator spawnThreat()
    {
        int timeToSpawn = Random.Range(0, 3);
        yield return new WaitForSeconds(timeToSpawn);
        int randomXPosition = Random.Range(-275, 275);
        int randomThreatPrefabNum = Random.Range(0, threats.Length);
        Instantiate(threats[randomThreatPrefabNum], new Vector3(randomXPosition, this.transform.position.y, this.transform.position.z), threats[randomThreatPrefabNum].transform.rotation);
        StartCoroutine(spawnThreat());

    }
}
