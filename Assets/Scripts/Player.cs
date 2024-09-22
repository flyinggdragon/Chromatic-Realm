using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Private
    private float _speed = 15f;
    private float _jumpForce = 8f; 
    private bool _grounded = true;

    // Public
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }

    // Temporário
    private int i = 0;
    public ColorName startingColorName;
    public ColorAttr colorAttr { get; private set; }
    public Color color { get; private set; }

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Teste
        colorAttr = ChrColor.FindColorAttr(startingColorName);
        color = colorAttr.rgbValue;
        sr.color = color;
    }

    private void UpdateColor() {
        
    }
    
    private void Update() {
        
    }

    private void FixedUpdate() {
        Move();
        HandleInput();
    }

    private void Move() {
        // Movimentação lateral do personagem
        float hInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(hInput * _speed, rb.velocity.y);
        rb.velocity = movement;
    }

    private void HandleInput() {
        // Pulo
        if (Input.GetKey(KeyCode.Space) && _grounded) {
            rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            _grounded = false;
        }

        // Trocar Cor
        if (Input.GetKeyDown(KeyCode.C)) {
            if (i > ChrColor.colors.Count) { i = -1; }

            i++;

            colorAttr = ChrColor.colors[i];
            color = colorAttr.rgbValue;

            sr.color = color;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _grounded = true;
        }
    }
}