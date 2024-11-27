using UnityEngine;
using TMPro;

public class TopBar : UIElement {
    [SerializeField] TMP_Text cccUses;
    [SerializeField] Timer timer;

    private void Update() {
        if (GameManager.chromaticCircleUses == -1) {
            cccUses.text = "∞";
        }

        else if (GameManager.chromaticCircleUses >= 0) {
            cccUses.text = GameManager.chromaticCircleUses.ToString();
        }
    }
}