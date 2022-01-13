using UnityEngine;

public class Flight : MonoBehaviour {
    [HideInInspector] public bool isFlying = false;
    public bool isLanding = false;
    public HangarManager hangarManager;
    private Vector3 lastPosition;
    private float distanceTraveled = 0;
    private float flyingSpeed = 1;
    private float landingSpeed = 100;

    private void Start() {
        hangarManager = HangarManager.instance;
    }

    private void Update() {
        if (isFlying) Fly();
        if (isLanding) Land();
    }

    private void Fly() {
        UpdateDistance();
        if (distanceTraveled < 150) {
            transform.Translate(Vector3.forward * Time.deltaTime * flyingSpeed);
            flyingSpeed += 0.5f;
            return;
        }
        gameObject.GetComponent<ShipDrag>().isInSpace = true;
        gameObject.GetComponent<ShipDrag>().timeStamp = Mathf.Round(Time.time + GetTimeInSpace());
        hangarManager.SaveSpaceShip(gameObject);
        Destroy(gameObject);
    }

    private void Land() {
        UpdateDistance();
        if (distanceTraveled < 200) {
            transform.Translate(Vector3.forward * Time.deltaTime * landingSpeed);
            if (landingSpeed > 1) landingSpeed -= 0.1f;
            return;
        }
        isLanding = false;
        distanceTraveled = 0;
    }

    private float GetTimeInSpace() {
        float time = Random.Range(5, 10); //Random.Range(120, 181);
        float energy = gameObject.GetComponent<ShipDrag>().energy;
        time *= energy / 100;
        Debug.Log("energy: " + energy + ", time: " + time);
        return time;
    }

    private void UpdateDistance() {
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }
}
