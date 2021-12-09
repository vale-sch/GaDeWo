using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour {
    [HideInInspector]
    public bool isClicked;
    public void onButtonClick() {
        isClicked = true;
    }
}
