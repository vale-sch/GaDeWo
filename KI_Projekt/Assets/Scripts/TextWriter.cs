using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour {
    private Text uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters;
    private string textWritten;

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, string textWritten) {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        this.textWritten = textWritten;
        characterIndex = 0;
    }

    private void Update() {
        if (uiText != null) {
            timer -= Time.deltaTime;
            while (timer <= 0f) {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                text = textWritten + text + "█";
                if (invisibleCharacters) text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                uiText.text = text;

                if (characterIndex >= textToWrite.Length) {
                    TerminalText.isNextTextReady = true;
                    uiText = null;
                    return;
                }
            }
        }
    }
}
