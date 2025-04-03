using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject textboxPrefab;
    public Pause pause;
    public ChromaticCircle chromaticCircle;
    public static bool uiOpen;

    public IEnumerator InstantiateWindow(List<string> strings, float waitTime = 2f) {
        GameObject textboxInstance = Instantiate(textboxPrefab, transform);

        Textbox textbox = textboxInstance.GetComponent<Textbox>();
        textbox.textList = strings;

        yield return StartCoroutine(textbox.DisplayTextAndWait(waitTime));
    }

    public void TogglePause() {
        pause.GetComponent<Pause>().ToggleVisibility();
    }

    public IEnumerator DisplayVictory() {
        List<string> strings = new List<string> {"Level Completed"};
        strings[0] = "Level Completed!";

        yield return StartCoroutine(InstantiateWindow(strings, 5f));
    }
}