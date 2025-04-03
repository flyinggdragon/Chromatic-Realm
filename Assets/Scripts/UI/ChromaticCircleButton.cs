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
    [SerializeField] public AudioClip selectSFX;
    [SerializeField] public AudioClip hoverSFX;

    private void Start() {
        chromaticCircle = GetComponentInParent<ChromaticCircle>();
    }

    public void MouseClick() {
        chromaticCircle.ColorSelect(buttonColor);
        AudioManager.Instance.PlaySFX(selectSFX);
    }

    public void MouseEnter() {
        chromaticCircle.ColorHighlight(buttonColor);
        AudioManager.Instance.PlaySFX(hoverSFX);
    }
}