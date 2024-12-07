using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Textbox : UIElement {
    public static bool inDialogue = false;
    public TMP_Text textObj;
    public List<string> textList { get; set; }
    public System.Action OnTextComplete;

    protected override void Update() {
        inDialogue = isOpen;
    }

    public IEnumerator DisplayTextAndWait(float waitTime = 2f) {
        GameManager.ResetMovement();

        isOpen = true;
        Player.shouldMove = false;
        Player.shouldInput = false;
        UIManager.uiOpen = true;

        foreach (var str in textList) {
            textObj.text = str;

            yield return new WaitForSeconds(waitTime);

            while (!shouldClose) {
                if (Input.GetMouseButtonDown(0)) {
                    shouldClose = true;
                }

                yield return null;
            }

            shouldClose = false;
        }

        OnTextComplete?.Invoke();

        isOpen = false;
        UIManager.uiOpen = false;
        Player.shouldMove = true;
        Player.shouldInput = true;

        SelfDestroy();
    }

    public override void SelfDestroy() {
        Destroy(gameObject);
    }

    public void Test() {
        Debug.Log("Test");
    }
}   