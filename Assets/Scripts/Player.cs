using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Private
    private Color _color { get; }
    private float speed = 15f;
    private float jumpForce = 8f; 
    private bool grounded = true;

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
        HandleInput();
    }

    private void Move() {
        // Movimentação lateral do personagem
        float hInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(hInput * speed, rb.velocity.y);

        rb.velocity = movement;
    }

    private void HandleInput() {
        // Pulo
        if (Input.GetKey(KeyCode.Space) && grounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

            grounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
    }
}