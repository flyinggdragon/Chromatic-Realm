using UnityEngine;

public interface ICanColorChange {
    public ColorAttr colorAttr { get; set; }
    public void ChangeColor(ColorAttr newColorAttr);
}