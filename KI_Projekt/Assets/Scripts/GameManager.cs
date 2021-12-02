using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    public string mainScene;
    public void OnClick() {
        SceneManager.LoadScene(mainScene);
    }
}
