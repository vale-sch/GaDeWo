using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TerminalText : MonoBehaviour {
    public VolumeRealtimeEdit volumeRealtimeEdit;
    [SerializeField] private TextWriter textWriter;
    public Text terminalText;
    public static bool isNextTextReady = true;
    //private string[] textArray = new string[7];
    [TextArea] public string[] textArr;
    private string textWritten;
    private int arrIndex;

    private bool isDisabled;

    public AudioSource bootingSound;
    public AudioSource cantinaChatter;

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
        if (!bootingSound.isPlaying) bootingSound.Play();
        StartCoroutine(playCantinaChatter());
    }

    private void Update() {
        if (arrIndex > textArr.Length - 1 && !isDisabled)
            StartCoroutine(DisableMyself());


        if (isNextTextReady && !isDisabled) {
            if (arrIndex < 5) StartCoroutine(WriteText(textArr[arrIndex], 0.2f));
            else StartCoroutine(WriteText(textArr[arrIndex], 0));
        }
    }
    private IEnumerator playCantinaChatter() {
        yield return new WaitForSeconds(20);
        cantinaChatter.Play();
    }
    private IEnumerator WriteText(string text, float secondsToWait) {
        isNextTextReady = false;
        yield return new WaitForSeconds(secondsToWait);
        textWriter.AddWriter(terminalText, text, 0.07f, true, textWritten);
        if (arrIndex <= textArr.Length - 1) textWritten += textArr[arrIndex];
        arrIndex++;
    }
    IEnumerator DisableMyself() {
        isDisabled = true;
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponentInChildren<Image>().enabled = false;
        this.gameObject.GetComponentInChildren<Text>().enabled = false;
        this.transform.GetChild(this.transform.childCount - 1).gameObject.SetActive(true);
        this.enabled = false;
        volumeRealtimeEdit.enabled = true;
    }
}
