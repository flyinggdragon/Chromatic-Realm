using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ColorInterface: UIElement {
    [SerializeField] private Transform colorPicker;
    [SerializeField] private Transform colorInfo;
    [SerializeField] private Player player;
    private bool colorSelected = false;

    protected override void Start() {
        ColorHighlight(player.currentColorName);
    }

    public void ColorHighlight(ColorName colorName) {
        ColorAttr colorAttr = ChrColor.FindColorAttr(colorName);

        Transform infoContainer = colorInfo.GetChild(0).transform;

        Transform colorTitle = infoContainer.GetChild(0).transform;
        Transform colorSubattr = colorTitle.GetChild(1).transform;
        Transform harmoniesContainer = infoContainer.GetChild(1).transform;

        TMP_Text colorNameTitle = colorTitle.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text colorTemperatureTitle = colorSubattr.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text colorTypeTitle = colorSubattr.GetChild(1).GetComponent<TMP_Text>();

        // Nome da cor
        colorNameTitle.text = colorName.ToString();
        colorNameTitle.color = colorAttr.rgbValue;

        // Temperatura da cor
        ColorTemperature ct = colorAttr.colorTemperature;
        colorTemperatureTitle.text = ct.ToString();
        
        switch(ct) {
            case ColorTemperature.Warm:
                colorTemperatureTitle.color = ChrColor.Red.rgbValue;
                break;
            case ColorTemperature.Cool:
                colorTemperatureTitle.color = ChrColor.Blue.rgbValue;
                break;
            default:
                colorTemperatureTitle.color = ChrColor.White.rgbValue;
                break;
        }

        // Tipo da cor
        colorTypeTitle.text = colorAttr.colorType.ToString();

        // Harmonias
        Transform complementary = harmoniesContainer.GetChild(0);
        Transform analogue = harmoniesContainer.GetChild(1);
        Transform triadic = harmoniesContainer.GetChild(2);
        
        // FUTURO: Para os 3. Se passar o mouse por cima (hover), mostrar janelinha com o nome da cor em cima?
        // Complementar
        Image compImg = complementary.GetComponentInChildren<Image>();
        ColorAttr compColor = ChrColor.FindColorAttr(colorAttr.complementaryColor);

        compImg.color = compColor.rgbValue;

        // Análoga
        Image[] analogImgs = analogue.GetComponentsInChildren<Image>();

        for (int i = 0; i < analogImgs.Length; i++) {
            analogImgs[i].color = ChrColor.FindColorAttr(colorAttr.analogueColors[i]).rgbValue;
        }
        
        // Triádica
        Image[] triadicImgs = triadic.GetComponentsInChildren<Image>();

        for (int i = 0; i < triadicImgs.Length; i++) {
            triadicImgs[i].color = ChrColor.FindColorAttr(colorAttr.triadicColors[i]).rgbValue;
        }
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
        shouldClose = false;

        colorSelected = false;
        
        obj.SetActive(true);
        currentlyActive = true;

        while (!colorSelected) {
            yield return null;
        }

        obj.SetActive(false);
        currentlyActive = false;

        GameManager.EnableMovement();

        shouldClose = true;
    }
}