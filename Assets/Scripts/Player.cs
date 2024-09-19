using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Private
    private Color _color { get; }
    private float speed = 15f;

    // Public
    public Rigidbody2D rb;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        // Movimentação lateral do personagem
        float hInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(hInput * speed, rb.velocity.y);

        rb.velocity = movement;
    }
}