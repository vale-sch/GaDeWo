using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualEnviromentMaker: MonoBehaviour {
    public GameObject[] spielAbschnitte;
    private GameObject oldAbschnitt;
    private int counter;

    private void Start() {
        GenerateSpielAbschnitte(spielAbschnitte);
    }
    private void GenerateSpielAbschnitte(GameObject[] _spielAbschnitte) {
        foreach (GameObject spielAbschnitt in spielAbschnitte) {
            if (counter == 0)
                Instantiate(spielAbschnitt, Vector3.zero, Quaternion.identity).transform.parent = this.transform;
            else {
                if (oldAbschnitt != null)
                    oldAbschnitt = Instantiate(spielAbschnitt, new Vector3(spielAbschnitt.transform.position.x, oldAbschnitt.transform.position.y + 11000, spielAbschnitt.transform.position.z), Quaternion.identity);
                else
                    oldAbschnitt = Instantiate(spielAbschnitt, new Vector3(spielAbschnitt.transform.position.x, 11000, spielAbschnitt.transform.position.z), Quaternion.identity);
                oldAbschnitt.transform.parent = this.transform;
            }
            counter++;
        }
    }
}

