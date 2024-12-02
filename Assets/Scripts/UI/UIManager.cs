using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject textboxPrefab;
    public GameObject quitModalPrefab;
    public GameObject pause;

    public IEnumerator InstantiateWindow(List<string> strings) {
        GameObject textboxInstance = Instantiate(textboxPrefab, transform);

        Textbox textbox = textboxInstance.GetComponent<Textbox>();
        textbox.textList = strings;

        yield return StartCoroutine(textbox.DisplayTextAndWait());
    }

    public void TogglePause() {
        pause.GetComponent<Pause>().Toggle();
    }

    public IEnumerator DisplayVictory() {
        List<string> strings = new List<string> {"Level Completed"};
        strings[0] = "Level Completed!";

        yield return StartCoroutine(InstantiateWindow(strings));
    }
}