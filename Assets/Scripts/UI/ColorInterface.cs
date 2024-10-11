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
        ColorAllSquares();
        ColorHighlight(player.currentColorName);
    }

    private void ColorAllSquares() {
        // Pinta todos os botões com as cores do ChrColor
        Transform buttonContainer = colorPicker.GetChild(0).GetChild(1);
        
        Button[] buttonArray = buttonContainer.GetComponentsInChildren<Button>();

        foreach (Button button in buttonArray) {
            ColorAttr paintColor = ChrColor.colors[button.transform.GetSiblingIndex()];

            Image img = button.GetComponent<Image>();

            img.color = paintColor.rgbValue;
            button.gameObject.name = paintColor.chrColorName.ToString();

            // Adiciona um listener que ao ser clicado seleciona nova cor.
            button.onClick.AddListener(() => {
                ColorSelect(paintColor.chrColorName);
            });

            // Adiciona um listener do mouse hover.
            AddHoverEvent(button, paintColor.chrColorName);
        }
    }

    private void AddHoverEvent(Button button, ColorName colorName) {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Evento para quando o mouse passar por cima (PointerEnter).
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;

        pointerEnter.callback.AddListener((eventData) => {
            ColorHighlight(colorName);
        });

        trigger.triggers.Add(pointerEnter);

        // Evento para quando o mouse sair (PointerExit).
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;

        pointerExit.callback.AddListener((eventData) => {
            ResetHighlight();
        });

        trigger.triggers.Add(pointerExit);
    }

    private void ColorHighlight(ColorName colorName) {
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
                colorTemperatureTitle.color = ChrColor.RedOrange.rgbValue;
                break;
            case ColorTemperature.Cool:
                colorTemperatureTitle.color = ChrColor.Turquoise.rgbValue;
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

    private void ResetHighlight() {

    }

    private void ColorSelect(ColorName colorName) {
        player.ChangeColor(ChrColor.FindColorAttr(colorName));
        colorSelected = true;
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

        player.EnableMovement();

        shouldClose = true;
    }
}