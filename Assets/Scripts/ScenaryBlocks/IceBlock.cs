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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("ThePlayer")) {
            ColorAttr objAttr = other.gameObject.GetComponent<Block>()?.colorAttr ?? other.gameObject.GetComponent<Player>()?.colorAttr;

            if (objAttr.colorTemperature is ColorTemperature.Warm) {
                Melt();
            }
        }
    }
}