using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HangarUI : MonoBehaviour {
    public static HangarUI instance;
    public Text textField;
    [HideInInspector] public string transferText = "SUPPLIES TRANSFERRED TO CARGO";
    [HideInInspector] public string spaceErrorText = "SHIP NEEDS TO BE UNLOADED AND/OR CHARGED FIRST";

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one TransferUI in scene!");
            return;
        }
        instance = this;
    }

    public void DisplayText(string s) {
        textField.text = s;
        StartCoroutine(DisableText());
    }

    private IEnumerator DisableText() {
        yield return new WaitForSeconds(4);
        textField.text = null;
    }
}
