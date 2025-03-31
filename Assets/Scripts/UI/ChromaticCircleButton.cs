using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChromaticCircleButton : MonoBehaviour {
    [SerializeField] private ColorName buttonColor;
    private ColorInterface colorInterface;

    private void Start() {
        colorInterface = GetComponentInParent<ColorInterface>();
    }
    public void MouseClick() {
        Debug.Log("Clicou no red");
        colorInterface.ColorSelect(buttonColor);
    }

    public void MouseEnter() {
        colorInterface.ColorHighlight(buttonColor);
    }
}