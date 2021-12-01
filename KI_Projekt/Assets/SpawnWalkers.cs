using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWalkers : MonoBehaviour {
    public GameObject walker;
    public Transform[] wayPoints;
    void Start() {
        StartCoroutine(spawnWalker());
    }

    IEnumerator spawnWalker() {
        yield return new WaitForSeconds(Random.Range(5, 10));
        var walkerCache = Instantiate(walker, this.transform.position, walker.transform.rotation);
        int increment = 0;
        foreach (var waypoint in wayPoints) {
            walkerCache.GetComponent<WalkBehaviour>().waypoints[increment] = waypoint;
            increment++;
        }
        walkerCache.GetComponent<WalkBehaviour>().enabled = true;
        StartCoroutine(spawnWalker());
    }
}
