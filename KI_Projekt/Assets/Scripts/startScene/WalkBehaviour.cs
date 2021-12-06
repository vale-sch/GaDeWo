using UnityEngine;
using UnityEngine.UI;
using System;
public class WalkBehaviour : MaybePhysical {
    public Transform[] waypoints;
    public Image imageCircle;
    public GameObject textMesh;
    public string[] infoNames;
    public string[] infoAge;
    public string[] infoLoves;
    private string[] infosArray;
    uint waypointIndex = 0;
    public float speed;
    public bool repeat;
    public bool shouldFollowWaypoints;
    [HideInInspector]
    public bool isServed;
    void Start() {
        infosArray = new string[infoLoves.Length];
        for (int i = 0; i < infosArray.Length; i++) {
            infosArray[i] = infoNames[i] + "\n" + "Alter: " + infoAge[i] + "\n" + "Vorliebe: " + infoLoves[i];
        }
        textMesh.GetComponent<TextMesh>().text = infosArray[UnityEngine.Random.Range(0, infosArray.Length)];
    }
    void FixedUpdate() {
        if (shouldFollowWaypoints) {
            if (waypointIndex >= waypoints.Length) return;

            var dir = (waypoints[waypointIndex].position - transform.position);
            float factor = speed * Time.fixedDeltaTime / dir.magnitude;

            SetPosition(transform.position + dir.normalized * speed * Time.fixedDeltaTime);

            if (factor >= 0.5f) {
                SetRotation(waypoints[waypointIndex++].rotation);
                if (waypointIndex == waypoints.Length) {
                    waypointIndex = 0;
                    if (!repeat)
                        enabled = false;

                }
            } else SetRotation(Quaternion.Slerp(transform.rotation, waypoints[waypointIndex].rotation, factor));
        }

    }

}
