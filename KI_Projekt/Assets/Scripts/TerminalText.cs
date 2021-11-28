using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TerminalText : MonoBehaviour {
    //public static TerminalText instance; // static bool isNextTextReady
    [SerializeField] private TextWriter textWriter;
    public Text terminalText;
    public static bool isNextTextReady = true;
    //private string[] textArray = new string[7];
    [TextArea] public string[] textArr;
    private string textWritten;
    private int arrIndex;

    /* private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one TerminalText in scene!");
            return;
        }
        instance = this;
    }
 */

    private void Start() {
        for (int i = 0; i <= textArr.Length - 1; i++) {
            textArr[i] = textArr[i].Replace("NEWLINE", "\n");
        }
        /* textArray[0] = "Initializing firmware...";
        textArray[1] = "\n> Initialization complete.\n\nSystem check...";
        textArray[2] = "\n> Passed.\n\nLoading Super_Barista_AI.exe...";
        textArray[3] = "\n> v4.81.516.2342 loaded.\n\nStarting modules...";
        textArray[4] = "\n> water_pump ready.";
        textArray[5] = "\n> heat_exchanger ready.";
        textArray[6] = "\n> front_camera ready."; */

        arrIndex = 0;
        textWritten = null;
    }

    private void Update() {
        if (arrIndex > textArr.Length - 1) return;
        if (isNextTextReady) {
            if (arrIndex < 5) StartCoroutine(WriteText(textArr[arrIndex], 1));
            else StartCoroutine(WriteText(textArr[arrIndex], 0));
        }
    }

    private IEnumerator WriteText(string text, int secondsToWait) {
        isNextTextReady = false;
        yield return new WaitForSeconds(secondsToWait);
        textWriter.AddWriter(terminalText, text, 0.08f, true, textWritten);
        if (arrIndex <= textArr.Length - 1) textWritten += textArr[arrIndex];
        arrIndex++;
    }
}
