using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Private
    private float _speed = 15f;
    private float _jumpForce = 10f; 
    private Color32 _color;

    // Public
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public bool grounded = true;
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; private set; }

    // Temporário
    private int i = 0;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Atualiza a cor para a cor escolhida no Inspector.
        ChangeColor(ChrColor.FindColorAttr(currentColorName));
    }

    public void ChangeColor(ColorAttr newColorAttr) {
        colorAttr = newColorAttr;
        _color = colorAttr.rgbValue;
        sr.color = _color;
    }

    private void FixedUpdate() {
        Move();
        HandleInput();
    }

    private void Move() {
        // Movimentação lateral do personagem
        float hInput = Input.GetAxis("Horizontal");

        Vector2 movement = new(hInput * _speed, rb.velocity.y);
        rb.velocity = movement;
    }

    private void HandleInput() {
        // Pulo
        if (Input.GetKey(KeyCode.Space) && grounded) {
            Jump(_jumpForce);
        }
    }

    public void Jump(float jumpForce) {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = true;
        }

        if (other.gameObject.CompareTag("Block")) {
            if (other.gameObject.GetComponent<Block>() is not SoftBlock) return;

            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground") ) {
            grounded = false;
        }

        if (other.gameObject.CompareTag("Block")) {
            if (other.gameObject.GetComponent<Block>() is not SoftBlock) return;

            grounded = false;
        }
    }
}