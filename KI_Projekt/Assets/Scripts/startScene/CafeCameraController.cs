
using UnityEngine;
using UnityEngine.UI;

public class CafeCameraController : MonoBehaviour {
    public SpawnWalkers spawnWalkers;
    private bool fillAmountFull;
    void Update() {
        if (spawnWalkers.walkerCache) {
            transform.LookAt(spawnWalkers.walkerCache.transform, this.transform.up);
            if (!fillAmountFull)
                if (spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().imageCircle.fillAmount <= 0.99)
                    spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().imageCircle.fillAmount += Time.deltaTime * 0.25f;
                else {
                    fillAmountFull = true;
                    spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().imageCircle.fillAmount = 0;
                    spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().textMesh.SetActive(true);
                }
            return;
        }
        float mouseY = Input.GetAxis("Mouse Y");
        float rotX = this.transform.rotation.eulerAngles.x;
        if (mouseY > 0 && rotX > 5 || mouseY < 0 && rotX < 20)
            transform.Rotate(new Vector3(-mouseY * 0.75f, 0, 0));

        float mouseX = Input.GetAxis("Mouse X");
        float rotY = this.transform.rotation.eulerAngles.y;
        if (mouseX > 0 && rotY < 180 || mouseX < 0 && rotY > 100)
            transform.Rotate(new Vector3(0, mouseX * 0.75f, 0));
    }
    public void onButtonClick(string buttonText) {
        if (spawnWalkers.walkerCache) {
            if (spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().infoLove.Equals(buttonText)) {
                fillAmountFull = false;
                spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().isServed = true;
                spawnWalkers.walkerCache.GetComponent<MeshRenderer>().material.color = Color.green;
            } else {
                fillAmountFull = false;
                spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().isServed = true;
                spawnWalkers.walkerCache.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
