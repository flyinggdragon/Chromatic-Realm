using System;
using System.Collections.Generic;
using UnityEngine;

public class MixRay : MonoBehaviour, IColorChanger {
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; protected set; }
    public SpriteRenderer sr { get; protected set; }

    public Color _color;

    public void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();

        // Atualiza a cor para a cor escolhida no Inspector.
        colorAttr = ChrColor.FindColorAttr(currentColorName);

        currentColorName = colorAttr.chrColorName;
        _color = colorAttr.rgbValue;
        _color = new Color(_color.r, _color.g, _color.b, 0.7f);
        sr.color = _color;
    }

    public void CauseColorChange(GameObject obj) {
        ColorAttr objColorAttr = obj.GetComponent<ICanColorChange>().colorAttr;

        ColorAttr newColor = ChrColor.GetColorMix(colorAttr.chrColorName, objColorAttr.chrColorName);

        obj.GetComponent<ICanColorChange>().ChangeColor(newColor);
    }
}