using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlock : MonoBehaviour {
    private Vector2 position;
    [SerializeField] private GameObject iceBlockPrefab;
    public Collider2D blockCollider;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    private void Freeze() {
        Destroy(gameObject);
        Instantiate(iceBlockPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("ThePlayer")) {
            Player player = other.gameObject.GetComponent<Player>();
            Block block = other.gameObject.GetComponent<Block>();

            player.grounded = true;
            ColorAttr objAttr = player?.colorAttr ?? block?.colorAttr;

            if (player is not null) {
                player.rb.gravityScale = 0.2f;
                player.rb.drag = 5f;
            }

            if (objAttr.colorTemperature is ColorTemperature.Cool) {
                Freeze();
            }
        }
    }

    private void OnTriggerExit2D() {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.grounded = false;

        player.rb.gravityScale = 1f;
        player.rb.drag = 0.05f;
    }
}