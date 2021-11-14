using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpawner : MonoBehaviour
{
    public GameObject containerPrefab;
    public Color[] colArray;
    public Transform[] spawnPoints;
    void Start()
    {
        StartCoroutine(spawnContainers());
    }

    IEnumerator spawnContainers()
    {
        float randomTime = Random.Range(4f, 8f);

        yield return new WaitForSeconds(randomTime);
        int randomNum = Random.Range(0, 5);
        GameObject instContainer = Instantiate(containerPrefab, spawnPoints[randomNum].position, Quaternion.identity);
        int randomCol = Random.Range(0, 5);
        foreach (Transform contChild in instContainer.transform.GetChild(1).transform)
        {
            contChild.GetComponent<MeshRenderer>().material.color = colArray[randomCol];
        }

        StartCoroutine(spawnContainers());

    }

}
