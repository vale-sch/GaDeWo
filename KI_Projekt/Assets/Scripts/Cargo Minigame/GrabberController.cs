using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float minX = -40f;
    public float maxX = 100f;
    public float minY = 5f;
    public float maxY = 70f;
    public float minZ = -40f;
    public float maxZ = 50f;
    public bool hasGrabbed = false;
    private GameObject container;
    private RaycastHit ray;
    void Update()
    {
        CheckInput();


    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.z < maxZ) transform.parent.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z > minZ) transform.parent.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < maxX) transform.parent.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > minX) transform.parent.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (transform.position.y < maxY) transform.parent.transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (transform.position.y > minY) transform.parent.transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.Space) && container && hasGrabbed)
        {
            hasGrabbed = false;
            container.transform.parent = null;
            StartCoroutine(releaseGrabDelay());

        }
    }
    void OnTriggerEnter(Collider _container)
    {
        if (_container.GetComponent<ContainerColl>() && !_container.GetComponent<ContainerColl>().isGrabbed && !hasGrabbed)
        {
            hasGrabbed = true;
            _container.GetComponent<ContainerColl>().isGrabbed = true;
            _container.transform.parent.GetComponent<Rigidbody>().isKinematic = true;

            _container.enabled = false;
            _container.transform.parent.transform.GetChild(1).GetChild(0).GetComponent<Collider>().enabled = false;

            _container.transform.parent.transform.position = this.transform.parent.GetChild(0).transform.position;
            _container.transform.parent.transform.SetParent(transform.parent);
            container = _container.transform.parent.gameObject;
        }
    }
    IEnumerator releaseGrabDelay()
    {
        GameObject oldContainer = container;
        container = null;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out ray, Mathf.Infinity))
        {
            GameObject hitObject = ray.collider.gameObject;
            if (hitObject.GetComponent<GridControl>())
                if (!hitObject.GetComponent<GridControl>().hasContainer)
                {
                    hitObject.GetComponent<GridControl>().hasContainer = true;
                    GameData.SaveNodeStatus(hitObject.transform.GetSiblingIndex(), true);
                    oldContainer.transform.position = hitObject.transform.position;
                    UpdateContainerposition(oldContainer);
                }
            if (hitObject.GetComponent<GridControlContainer>())
                if (!hitObject.GetComponent<GridControlContainer>().hasContainer)
                {
                    hitObject.GetComponent<GridControlContainer>().hasContainer = true;
                    foreach (Container container in GameData.containers)
                    {
                        if (container.name == hitObject.name) container.hasContainer = true;
                    }
                    oldContainer.transform.position = hitObject.transform.GetChild(0).transform.position;
                    UpdateContainerposition(oldContainer);
                }
        }
        foreach (Collider item in oldContainer.GetComponentsInChildren<Collider>())
        {
            item.enabled = true;
        }
        oldContainer.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.75f);
        oldContainer.GetComponentInChildren<ContainerColl>().isGrabbed = false;
    }

    private void UpdateContainerposition(GameObject updatedContainer)
    {
        foreach (Container container in GameData.containers)
        {
            if (container.name == updatedContainer.name) container.position = updatedContainer.transform.position;
        }
    }
}
