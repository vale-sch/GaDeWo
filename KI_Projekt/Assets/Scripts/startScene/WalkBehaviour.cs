using UnityEngine;

public class WalkBehaviour : MaybePhysical {
    public Transform[] waypoints;
    uint waypointIndex = 0;
    public float speed;
    public bool repeat;
    public bool shouldFollowWaypoints;

    Transform currentWaypoint, nextWaypoint;

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
                    if (!repeat) enabled = false;
                }
            } else SetRotation(Quaternion.Slerp(transform.rotation, waypoints[waypointIndex].rotation, factor));
        }

    }
}
