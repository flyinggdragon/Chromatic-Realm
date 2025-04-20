using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    private List<SpriteRenderer> srl;
    private List<Image> imgl;
    public float duration = 1f;

    private void Start() {
        StartCoroutine(FadeIn());
    }

    private void Awake() {
        srl = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        imgl = new List<Image>(GetComponentsInChildren<Image>());
    }
    public IEnumerator FadeIn() {
        float time = 0.0f;

        while (time < duration) {
            float alpha = Mathf.Lerp(0.0f, 1.0f, time / duration);
            
            if (imgl.Count > 0) {
                UIManager.uiOpen = true;

                foreach (Image img in imgl) {
                    Color newColor = img.color;
                    newColor.a = alpha;
                    img.color = newColor;
                }
            } else {
                foreach (SpriteRenderer sr in srl) {
                    Color newColor = sr.color;
                    newColor.a = alpha;
                    sr.color = newColor;
                }
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut() {
        float time = 0.0f;

        while (time < duration) {
            float alpha = Mathf.Lerp(1.0f, 0.0f, time / duration);
            
            if (imgl.Count > 0) {
                foreach (Image img in imgl) {
                    Color newColor = img.color;
                    newColor.a = alpha;
                    img.color = newColor;
                }
            } else {
                foreach (SpriteRenderer sr in srl) {
                    Color newColor = sr.color;
                    newColor.a = alpha;
                    sr.color = newColor;
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        UIManager.uiOpen = false;
        Destroy(gameObject);
    }
}