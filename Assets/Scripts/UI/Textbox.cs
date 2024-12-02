using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Textbox : UIElement {
    public TMP_Text textObj;
    public List<string> textList { get; set; }
    private bool clicked = false;
    public float waitTime = 2f;

    public System.Action OnTextComplete;

    protected override void Start() {
        
    }

    public IEnumerator DisplayTextAndWait() {
        foreach (var str in textList) {
            textObj.text = str;

            GameManager.DisableMovement();

            yield return new WaitForSeconds(waitTime);

            while (!clicked) {
                if (Input.GetMouseButtonDown(0)) {
                    clicked = true;
                }

                yield return null;
            }

            clicked = false;
        }

        GameManager.EnableMovement();
        OnTextComplete?.Invoke();
        SelfDestroy();
    }
}   