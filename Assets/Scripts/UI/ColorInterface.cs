using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ColorInterface: UIElement {
    [SerializeField] private Transform colorPicker;
    [SerializeField] private Transform colorInfo;
    [SerializeField] private Transform infoContainer;
    [SerializeField] private Transform colorTitle;
    [SerializeField] private Transform colorSubattr;
    [SerializeField] private Transform harmoniesContainer;
    private Player player;
    private bool colorSelected = false;

    protected override void Start() {
        player = GameObject.FindFirstObjectByType<Player>();
        ColorHighlight(player.currentColorName);
    }

    protected override void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ToggleVisibility();
        }
    }

    public void ColorHighlight(ColorName colorName) {
        ColorAttr colorAttr = ChrColor.FindColorAttr(colorName);

        TMP_Text titleTextComponent = colorTitle.GetComponent<TMP_Text>();
        TMP_Text temperatureTitleComponent = colorSubattr.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text typeTitleComponent = colorSubattr.GetChild(1).GetComponent<TMP_Text>();

        // Nome da cor
        titleTextComponent.text = colorName.ToString();
        titleTextComponent.color = colorAttr.rgbValue;

        // Temperatura da cor
        ColorTemperature ct = colorAttr.colorTemperature;
        temperatureTitleComponent.text = ct.ToString();
        
        switch(ct) {
            case ColorTemperature.Warm:
                temperatureTitleComponent.color = ChrColor.Red.rgbValue;
                break;
            case ColorTemperature.Cool:
                temperatureTitleComponent.color = ChrColor.Blue.rgbValue;
                break;
            default:
                temperatureTitleComponent.color = ChrColor.White.rgbValue;
                break;
        }

        // Tipo da cor
        typeTitleComponent.text = colorAttr.colorType.ToString();

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