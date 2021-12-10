using UnityEngine;

public class Flight : MonoBehaviour {
    [HideInInspector] public bool isFlight = false;
    private bool hasTargetPosition = false;
    private Vector3 targetPosition;
    private float flySpeed = 4;
    void Update() {
        if (isFlight) Fly();
    }

    private void Fly() {
        if (!hasTargetPosition) GetTargetPosition();


        //TODO: add force or velocity to rb (change prefab to point in right z-direction)
        if (Vector3.Distance(transform.position, targetPosition) > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, flySpeed * Time.deltaTime);
            return;
        }
        //Ship Taken Off Behaviour
        Destroy(gameObject);
    }

    private void GetTargetPosition() {
        Vector3 offset = new Vector3(-130, 0, 0);
        targetPosition = transform.position + offset;
        hasTargetPosition = true;
    }
}
