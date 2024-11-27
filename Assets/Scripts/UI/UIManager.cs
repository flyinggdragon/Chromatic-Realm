using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject textboxPrefab;

    public void InstantiateWindow(List<string> strings) {
        GameObject textboxInstance = Instantiate(textboxPrefab, transform);

        Textbox textbox = textboxInstance.GetComponent<Textbox>();
        textbox.textList = strings;
    }
}