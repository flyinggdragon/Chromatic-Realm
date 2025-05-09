using System;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public bool shouldGrab;
    public GameObject grabbingBlock = null;
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Block")) {
            shouldGrab = true;
            grabbingBlock = collider.gameObject;
            rb = grabbingBlock.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.CompareTag("Block")) {
            shouldGrab = false;
            grabbingBlock = null;
            rb = null;
        }
    }
}