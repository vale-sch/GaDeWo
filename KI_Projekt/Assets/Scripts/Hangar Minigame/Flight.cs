using UnityEngine;

public class Flight : MonoBehaviour {
    [HideInInspector] public bool isFlight = false;
    private Vector3 lastPosition;
    float distanceTraveled = 0;
    private float flySpeed = 10000;
    private Rigidbody rb;
    private void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update() {
        if (isFlight) Fly();
    }

    private void Fly() {
        UpdateDistance();
        if (distanceTraveled < 150) {
            rb.AddForce(transform.forward * Time.deltaTime * flySpeed);
            return;
        }
        //Ship Taken Off Behaviour
        Destroy(gameObject);
    }

    private void UpdateDistance() {
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }
}
