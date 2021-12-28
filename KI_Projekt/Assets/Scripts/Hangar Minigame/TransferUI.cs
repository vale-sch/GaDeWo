using System.Collections;
using UnityEngine;

public class TransferUI : MonoBehaviour {
    public GameObject transferText;

    private void Update() {
        if (GameData.newContainerInTransfer) {
            GameData.newContainerInTransfer = false;
            DisplayTransferText();
        }
    }
    public void DisplayTransferText() {
        transferText.SetActive(true);
        StartCoroutine(DisableTransferText());
    }

    private IEnumerator DisableTransferText() {
        yield return new WaitForSeconds(4);
        transferText.SetActive(false);
    }
}
