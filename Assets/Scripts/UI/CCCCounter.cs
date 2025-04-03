using UnityEngine;
using UnityEngine.UI;

public class CCCCounter : MonoBehaviour {
    [SerializeField] Text cccUses;
    private float hue = 0f;

    private void Update() {
        if (GameManager.chromaticCircleUses == -1) {
            cccUses.text = "∞";

            hue += Time.deltaTime * 0.5f;
            if (hue > 1f) hue -= 1f;

            cccUses.color = Color.HSVToRGB(hue, 1f, 1f);
        }

        if (GameManager.chromaticCircleUses == 0) {
            cccUses.text = GameManager.chromaticCircleUses.ToString();
            cccUses.color = Color.red;
        }

        else if (GameManager.chromaticCircleUses >= 0) {
            cccUses.text = GameManager.chromaticCircleUses.ToString();
            cccUses.color = Color.black;
        }
    }
}