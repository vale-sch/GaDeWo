using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject backgroundPrefab;

    public float speed;

    void Update()
    {
        this.transform.position = new Vector3(0f, this.transform.position.y - Time.deltaTime * speed, 0f);
    }
    void OnTriggerEnter(Collider spaceShip)
    {
        if (!spaceShip.GetComponent<SpaceShipShooter>()) return;
        Instantiate(backgroundPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 670f, this.transform.position.z), Quaternion.identity);
        StartCoroutine(DestroyMySelf());
    }
    IEnumerator DestroyMySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);


    }
}
