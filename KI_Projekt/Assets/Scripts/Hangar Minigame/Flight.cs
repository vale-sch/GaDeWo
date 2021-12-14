using UnityEngine;

public class Flight : MonoBehaviour {
    [HideInInspector] public bool isFlying = false;
    public bool isInSpace = false;
    private Vector3 lastPosition;
    float distanceTraveled = 0;
    private float flySpeed = 1;

    private void Update() {
        if (isFlying) Fly();
    }

    private void Fly() {
        UpdateDistance();
        if (distanceTraveled < 150) {
            transform.Translate(Vector3.forward * Time.deltaTime * flySpeed);
            flySpeed += 0.5f;
            return;
        }
        isInSpace = true;
        //Countdown till ship is back -> GameData (maybe Time.time - timestamp)
        //Save state to GameData
        Destroy(gameObject);
    }

    private void UpdateDistance() {
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }
}
