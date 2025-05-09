using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ChromaticCircle: UIElement {
    [SerializeField] private GameObject normalHighlight;
    [SerializeField] private Image backdropImg;
    [SerializeField] private Text colorName;
    [SerializeField] private Text colorType;
    [SerializeField] private GameObject notSelectable;
    [SerializeField] private Image nsBackdropImg;
    [SerializeField] private Text nsColorName;
    [SerializeField] private Transform complementaryColor;
    [SerializeField] private Transform analogColors;
    [SerializeField] private Transform triadicColors;
    private List<ChromaticCircleButton> buttons;
    private ChromaticCircleButton highlightedButton;
    private Player player;
    private bool colorSelected = false;

    protected override void Start() {
        buttons = new List<ChromaticCircleButton>(GetComponentsInChildren<ChromaticCircleButton>());

        for (int i = 0; i < buttons.Count; i++) {
            ChromaticCircleButton ccb = buttons[i];

            ccb.complementary = buttons[(i + 6) % 12];
            ccb.analogs.Add(buttons[(i + 11) % 12]);
            ccb.analogs.Add(buttons[(i + 1) % 12]);
            ccb.triadics.Add(buttons[(i + 4) % 12]);
            ccb.triadics.Add(buttons[(i + 8) % 12]);
        }

        player = GameObject.FindFirstObjectByType<Player>();
        ColorHighlight(player.currentColorName, true);
    }

    public override void ToggleVisibility() {
        if (shouldClose) {
            if (UIManager.uiOpen && !isOpen) return;

            isOpen = !isOpen;
            UIManager.uiOpen = isOpen;
            gameObject.SetActive(isOpen);

            Time.timeScale = isOpen ? 0.3f : 1.0f;

            Player.shouldMove = !isOpen;
            Player.shouldInput = !isOpen;

            if (isOpen) AudioManager.Instance.PlaySFX(openSFX);
            else AudioManager.Instance.PlaySFX(closeSFX);
        }
    }

    public void ColorHighlight(ColorName color, bool colorActive) {
        if (colorActive) {
            notSelectable.SetActive(false);
            normalHighlight.SetActive(true);

            highlightedButton = GetCCBByColor(color);
            ColorAttr highlightedColorAttr = ChrColor.FindColorAttr(color);

            backdropImg.color = new Color(highlightedColorAttr.rgbValue.r, highlightedColorAttr.rgbValue.g, highlightedColorAttr.rgbValue.b, 1/3f);

            colorName.text = highlightedColorAttr.name;
            colorName.color = highlightedColorAttr.rgbValue;

            colorType.text = highlightedColorAttr.colorType.ToString();

            complementaryColor.GetComponentsInChildren<Image>()[0].color = ChrColor.FindColorAttr(highlightedButton.complementary.buttonColor).rgbValue;
            complementaryColor.GetComponentInChildren<Text>().text = ChrColor.FindColorAttr(highlightedButton.complementary.buttonColor).name.Substring(0, 3) + ".";
            
            int i = 0;
            foreach (Transform gt in analogColors.transform) {
                GameObject g = gt.gameObject;

                g.GetComponentsInChildren<Image>()[0].color = ChrColor.FindColorAttr(highlightedButton.analogs[i].buttonColor).rgbValue;
                g.GetComponentInChildren<Text>().text = ChrColor.FindColorAttr(highlightedButton.analogs[i].buttonColor).name.Substring(0, 3) + ".";

                i++;
            }

            i = 0;
            foreach (Transform gt in triadicColors.transform) {
                GameObject g = gt.gameObject;

                g.GetComponentsInChildren<Image>()[0].color = ChrColor.FindColorAttr(highlightedButton.triadics[i].buttonColor).rgbValue;
                g.GetComponentInChildren<Text>().text = ChrColor.FindColorAttr(highlightedButton.triadics[i].buttonColor).name.Substring(0, 3) + ".";

                i++;
            }
        }

        else {
            normalHighlight.SetActive(false);
            notSelectable.SetActive(true);

            highlightedButton = GetCCBByColor(color);
            ColorAttr highlightedColorAttr = ChrColor.FindColorAttr(color);

            nsBackdropImg.color = new Color(highlightedColorAttr.rgbValue.r, highlightedColorAttr.rgbValue.g, highlightedColorAttr.rgbValue.b, 1/3f);

            nsColorName.text = highlightedColorAttr.name;
            nsColorName.color = highlightedColorAttr.rgbValue;
        }
    }
    
    private ChromaticCircleButton GetCCBByColor(ColorName colorName) {
        return buttons.Find(ccb => ccb.buttonColor == colorName);
    }

    public void ColorSelect(ColorName colorName) {

        player.ChangeColor(ChrColor.FindColorAttr(colorName));
        colorSelected = true;

        if (GameManager.chromaticCircleUses != -1) {
            GameManager.chromaticCircleUses -= 1;
        }

        ToggleVisibility();
    }

    public IEnumerator AsyncColorSelection() {
        GameManager.ResetMovement();

        shouldClose = false;
        colorSelected = false;
        
        isOpen = true;
        gameObject.SetActive(isOpen);

        while (!colorSelected) {
            yield return null;
        }

        isOpen = false;
        gameObject.SetActive(isOpen);

        shouldClose = true;
    }
}