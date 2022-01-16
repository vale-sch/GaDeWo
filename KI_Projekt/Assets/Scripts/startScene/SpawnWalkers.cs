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
        StartCoroutine(DestroyOldWalker(walkerCache));
        walkerCache = null;


    }
    IEnumerator DestroyOldWalker(GameObject _walkerOld) {
        int increment = 0;
        for (int i = wayPoints.Length - 1; i >= 0; i--) {
            _walkerOld.GetComponent<WalkBehaviour>().waypoints[increment] = wayPoints[i];
            increment++;
        }
        _walkerOld.GetComponent<WalkBehaviour>().enabled = true;
        do {
            yield return null;
        } while (_walkerOld.GetComponent<WalkBehaviour>().waypoints.Length - 1 != _walkerOld.GetComponent<WalkBehaviour>().waypointIndex);
        yield return new WaitForSeconds(0.25f);
        Destroy(_walkerOld);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(spawnWalker());
    }
}
