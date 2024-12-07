using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopBar : UIElement {
    [SerializeField] TMP_Text cccUses;
    [SerializeField] Image barBackground;

    protected override void Update() {
        if (GameManager.chromaticCircleUses == -1) {
            cccUses.text = "∞";
        }

        else if (GameManager.chromaticCircleUses >= 0) {
            cccUses.text = GameManager.chromaticCircleUses.ToString();
        }

        Player player = GameObject.FindFirstObjectByType<Player>();
        ColorAttr currentColor = ChrColor.FindColorAttr(player.currentColorName);

        Color newColor = currentColor.rgbValue;
        newColor.a = 0.8f;
        barBackground.color = newColor;
    }
}