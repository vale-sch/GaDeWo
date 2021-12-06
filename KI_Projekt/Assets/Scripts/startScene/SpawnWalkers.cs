using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWalkers : MonoBehaviour {
    public GameObject walker;
    [HideInInspector]
    public GameObject walkerCache;
    public Transform[] wayPoints;
    void Start() {
        StartCoroutine(spawnWalker());
    }

    IEnumerator spawnWalker() {

        walkerCache = Instantiate(walker, this.transform.position, walker.transform.rotation);
        int increment = 0;
        foreach (var waypoint in wayPoints) {
            walkerCache.GetComponent<WalkBehaviour>().waypoints[increment] = waypoint;
            increment++;
        }
        walkerCache.GetComponent<WalkBehaviour>().enabled = true;
        do {
            yield return null;
        } while (!walkerCache.GetComponent<WalkBehaviour>().isServed);
        Destroy(walkerCache);
        walkerCache = null;
        yield return new WaitForSeconds(Random.Range(5, 10));
        StartCoroutine(spawnWalker());
    }
}
