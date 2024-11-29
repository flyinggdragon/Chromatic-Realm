using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject textboxPrefab;
    public GameObject quitModalPrefab;
    public GameObject pause;

    public void InstantiateWindow(List<string> strings) {
        GameObject textboxInstance = Instantiate(textboxPrefab, transform);

        Textbox textbox = textboxInstance.GetComponent<Textbox>();
        textbox.textList = strings;
    }

    public void TogglePause() {
        pause.GetComponent<Pause>().Toggle();
    }
}