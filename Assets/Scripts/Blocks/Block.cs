using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    // Public
    public Collider2D blockCollider;
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; protected set; }
    public Rigidbody2D rb { get; protected set; }
    public SpriteRenderer sr { get; protected set; }
    
    // Protected
    protected Vector2 _position;
    protected Color32 _color;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Atualiza a cor para a cor escolhida no Inspector.
        colorAttr = ChrColor.FindColorAttr(currentColorName);

        currentColorName = colorAttr.chrColorName;
        _color = colorAttr.rgbValue;
        sr.color = _color;
    }

    public void ChangeColor(ColorAttr newColorAttr) {
        // Atualiza a cor para a cor recebida.
        colorAttr = newColorAttr;

        currentColorName = colorAttr.chrColorName;
        _color = colorAttr.rgbValue;
        sr.color = _color;

        // Atualiza também a cor de qualquer outro componente <Block> associado ao mesmo Object.
        foreach (Block block in GetComponents<Block>()) {
            if (block == this) continue;
            if (currentColorName == block.currentColorName) continue;

            block.colorAttr = newColorAttr;

            block.currentColorName = block.colorAttr.chrColorName;
            block._color = block.colorAttr.rgbValue;
            block.sr.color = block._color;
        }
    }
}