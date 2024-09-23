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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Atualiza a cor para a cor escolhida no Inspector.
        colorAttr = ChrColor.FindColorAttr(currentColorName);
        _color = colorAttr.rgbValue;
        sr.color = _color;
    }
}