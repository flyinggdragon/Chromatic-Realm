using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Textbox : UIElement {
    public TMP_Text textObj;
    public Image imageBackground;
    public List<string> textList;
    private bool clicked = false;
    public float waitTime = 2f;

    protected override void Start() {
        StartCoroutine(DisplayTextAndWait());
    }

    private IEnumerator DisplayTextAndWait() {
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
        SelfDestroy();
    }

    private void SelfDestroy() {
        Destroy(gameObject);
    }
}   