using System.Collections;
using UnityEngine;

public class GrabberController : MonoBehaviour {
    public float panSpeed = 30f;
    public float minX = -40f;
    public float maxX = 100f;
    public float maxY = 18f;
    public float minZ = -40f;
    public float maxZ = 50f;
    public bool hasGrabbed = false;
    private bool isGrabbing = false;
    private bool isReleasing = false;
    public GameObject grabHelper;
    private Renderer grabRend;
    private Color redGrabHelper;
    public Color greenGrabHelper;
    private GameObject container;
    private GameObject releaseHitObject = null;

    private void Start() {
        grabRend = grabHelper.GetComponent<Renderer>();
        redGrabHelper = grabRend.material.color;
    }

    private void Update() {
        if (ContainerUI.uiActive) return;
        if (isGrabbing) {
            Grab();
            return;
        }
        if (isReleasing) {
            Release();
            return;
        }
        CheckMovementInput();
        if (hasGrabbed) {
            CheckRelease();
            return;
        }
        CheckGrab();
    }

    private void CheckMovementInput() {
        if (Input.GetKey(KeyCode.W)) {
            if (transform.position.z < maxZ) transform.parent.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S)) {
            if (transform.position.z > minZ) transform.parent.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D)) {
            if (transform.position.x < maxX) transform.parent.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A)) {
            if (transform.position.x > minX) transform.parent.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }

    private void CheckGrab() {
        GameObject hitObject = GetRaycastHit().collider.gameObject;
        if (hitObject.GetComponent<CargoContainer>()) {
            grabRend.material.color = greenGrabHelper;
            if (Input.GetKey(KeyCode.Space)) {
                transform.parent.position = new Vector3(hitObject.transform.position.x, transform.parent.position.y, hitObject.transform.position.z);
                isGrabbing = true;
            }
            return;
        }
        grabRend.material.color = redGrabHelper;
    }

    private void Grab() {
        if (!hasGrabbed) {
            transform.parent.transform.Translate(Vector3.down * 5 * Time.deltaTime, Space.World);
            return;
        }
        if (transform.parent.position.y < maxY) {
            transform.parent.transform.Translate(Vector3.up * 5 * Time.deltaTime, Space.World);
            return;
        }
        grabRend.material.color = redGrabHelper;
        isGrabbing = false;
    }

    private void CheckRelease() {
        GameObject hitObject = GetRaycastHit().collider.gameObject;

        if ((hitObject.GetComponent<CargoNode>() || hitObject.GetComponent<CargoContainer>() && hitObject.transform.parent.GetComponent<CargoNode>() && hitObject.transform.parent.transform.childCount < 3) && AssertSize(hitObject)) {
            grabRend.material.color = greenGrabHelper;
            if (Input.GetKey(KeyCode.Space)) {
                transform.parent.position = new Vector3(hitObject.transform.position.x, transform.parent.position.y, hitObject.transform.position.z);
                isReleasing = true;
            }
            return;
        }
        if (hitObject.GetComponent<TransferNode>() && AssertDepartment(hitObject)) {
            grabRend.material.color = greenGrabHelper;
            if (Input.GetKey(KeyCode.Space)) {
                transform.parent.position = new Vector3(hitObject.transform.position.x, transform.parent.position.y, hitObject.transform.position.z);
                isReleasing = true;
                StartCoroutine(ProcessSupplies(hitObject));
            }
            return;
        }
        grabRend.material.color = redGrabHelper;
    }

    IEnumerator ProcessSupplies(GameObject node) {
        int containerSupplies = container.GetComponent<CargoContainer>().supplies;
        GameObject oldContainer = container;

        yield return new WaitUntil(() => !isReleasing);
        Destroy(oldContainer);
        node.GetComponent<TransferNode>().TransferSupplies(containerSupplies);
    }

    private void Release() {
        if (releaseHitObject == null) releaseHitObject = GetRaycastHit().collider.gameObject;
        if (hasGrabbed) {
            container.GetComponent<Collider>().enabled = true;

            CargoContainer cargoContainer = container.GetComponent<CargoContainer>();
            cargoContainer.isOnTriggerEnter = true;
            if (!cargoContainer.hasTriggered) {
                transform.parent.transform.Translate(Vector3.down * 5 * Time.deltaTime, Space.World);
                return;
            }
            cargoContainer.isOnTriggerEnter = false;
            cargoContainer.hasTriggered = false;
            hasGrabbed = false;

            if (releaseHitObject.GetComponent<CargoContainer>()) releaseHitObject = releaseHitObject.transform.parent.gameObject;
            container.transform.parent = releaseHitObject.transform;
            container = null;
        }
        if (transform.parent.position.y < maxY) {
            transform.parent.transform.Translate(Vector3.up * 5 * Time.deltaTime, Space.World);
            return;
        }
        releaseHitObject = null;
        isReleasing = false;
        grabRend.material.color = redGrabHelper;
    }

    IEnumerator releaseGrabDelay() {
        GameObject oldContainer = container;

        yield return new WaitForSeconds(0.5f);
        //oldContainer.GetComponent<Rigidbody>().isKinematic = false;
    }

    private RaycastHit GetRaycastHit() {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        Physics.Raycast(downRay, out hit);
        return hit;
    }

    void OnTriggerEnter(Collider _container) {
        if (_container.GetComponent<CargoContainer>() && !hasGrabbed) {
            hasGrabbed = true;
            //_container.transform.GetComponent<Rigidbody>().isKinematic = true;
            _container.enabled = false;
            _container.transform.SetParent(transform.parent);
            container = _container.gameObject;
        }
    }

    private bool AssertSize(GameObject objectToCompare) {
        ContainerSize objectSize;
        if (objectToCompare.GetComponent<CargoNode>() || objectToCompare.GetComponent<TransferNode>()) objectSize = objectToCompare.GetComponent<CargoNode>().size;
        else objectSize = objectToCompare.GetComponent<CargoContainer>().size;
        ContainerSize containerSize = container.GetComponent<CargoContainer>().size;

        if (objectSize == containerSize) return true;
        return false;
    }

    private bool AssertDepartment(GameObject node) {
        Department nodeDepartment = node.GetComponent<TransferNode>().department;
        Department containerDepartment = container.GetComponent<CargoContainer>().department;

        if (nodeDepartment == containerDepartment) return true;
        return false;
    }
}
