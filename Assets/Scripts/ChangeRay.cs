using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRay : MonoBehaviour, IColorChanger {
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; protected set; }
    public SpriteRenderer sr { get; protected set; }

    public Color _color;

    public void Start() {
        colorAttr = ChrColor.FindColorAttr(currentColorName);
        currentColorName = colorAttr.chrColorName;

        Transform ray = transform.GetChild(0);
        
        _color = colorAttr.rgbValue;
        ray.GetChild(0).transform.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 1f);
        ray.GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 0.5f);
        ray.GetChild(2).transform.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 0.35f);
        ray.GetChild(3).transform.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 0.2f);
    }

    public void CauseColorChange(GameObject obj) {
        obj.GetComponent<ICanColorChange>().ChangeColor(colorAttr);
    }
}