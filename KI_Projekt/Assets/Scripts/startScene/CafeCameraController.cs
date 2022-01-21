
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CafeCameraController : MonoBehaviour {
    public SpawnWalkers spawnWalkers;
    public Image imageFill;
    private bool fillAmountFull;
    private float popularityNmb;

    void Update() {
        if (spawnWalkers.walkerCache) {
            transform.LookAt(spawnWalkers.walkerCache.transform, this.transform.up);
            if (!fillAmountFull)
                if (spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().imageCircle.fillAmount <= 0.99)
                    spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().imageCircle.fillAmount += Time.fixedDeltaTime * 0.025f;
                else {
                    fillAmountFull = true;
                    spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().imageCircle.fillAmount = 0;
                    spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().textMesh.SetActive(true);
                }
            return;
        }
    }
    public void onButtonClick(string buttonText) {
        if (spawnWalkers.walkerCache) {
            if (spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().infoLove.Equals(buttonText)) {
                fillAmountFull = false;
                spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().isServed = true;
                spawnWalkers.walkerCache.GetComponent<MeshRenderer>().material.color = Color.green;
                popularityNmb += 0.2f;

            } else {
                fillAmountFull = false;
                spawnWalkers.walkerCache.GetComponent<WalkBehaviour>().isServed = true;
                spawnWalkers.walkerCache.GetComponent<MeshRenderer>().material.color = Color.red;
                popularityNmb -= 0.2f;
            }
            imageFill.fillAmount = popularityNmb;
            if (popularityNmb == 1)
                SceneManager.LoadScene("main");

        }
    }
}
