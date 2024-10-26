using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour {
    private Vector2 position;
    [SerializeField] private GameObject waterBlockPrefab;
    public Collider2D blockCollider;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    private void Melt() {
        Destroy(gameObject);
        Instantiate(waterBlockPrefab, transform.position, Quaternion.identity);
    }

    private void Update() {
        CheckContactWithObjects();
    }

    private void CheckContactWithObjects() {
        Collider2D[] contacts = new Collider2D[10];
        int contactCount = Physics2D.OverlapCollider(blockCollider, new ContactFilter2D(), contacts);

        for (int i = 0; i < contactCount; i++) {
            Collider2D contact = contacts[i];

            if (contact.CompareTag("ThePlayer") || contact.CompareTag("Block")) {
                if (Physics2D.IsTouching(blockCollider, contact)) {
                    ColorAttr objAttr = contact.GetComponent<Player>()?.colorAttr ?? contact.GetComponent<Block>()?.colorAttr;

                    if (objAttr.colorTemperature is ColorTemperature.Warm) {
                        Melt();
                        break;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("ThePlayer")) {
            Player player = other.gameObject.GetComponent<Player>();
            player.grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("ThePlayer")) {
            Player player = other.gameObject.GetComponent<Player>();
            player.grounded = false;
        }
    }
}