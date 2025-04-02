using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChromaticCircleButton : MonoBehaviour {
    [SerializeField] public ColorName buttonColor;
    private ChromaticCircle chromaticCircle;
    public ChromaticCircleButton complementary;
    public List<ChromaticCircleButton> analogs;
    public List<ChromaticCircleButton> triadics;
    [SerializeField] public Transform bottomExit;
    [SerializeField] public Transform leftExit;
    [SerializeField] public Transform rightExit;

    private void Start() {
        chromaticCircle = GetComponentInParent<ChromaticCircle>();
    }

    public void MouseClick() {
        chromaticCircle.ColorSelect(buttonColor);
    }

    public void MouseEnter() {
        chromaticCircle.ColorHighlight(buttonColor);
    }
}