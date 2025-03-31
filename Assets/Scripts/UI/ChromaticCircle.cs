using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChromaticCicle: UIElement {
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

    public void ColorHighlight(ColorName colorName) {
        
    }

    public void ColorSelect(ColorName colorName) {
        player.ChangeColor(ChrColor.FindColorAttr(colorName));
        colorSelected = true;

        if (GameManager.chromaticCircleUses != -1) {
            GameManager.chromaticCircleUses -= 1;
        }

        Debug.Log("Player clicou no " + colorName);
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