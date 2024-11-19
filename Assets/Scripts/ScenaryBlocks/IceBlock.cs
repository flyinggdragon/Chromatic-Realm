using UnityEngine;
using UnityEngine.Tilemaps;

public class IceBlock : MonoBehaviour {
    public Collider2D blockCollider;

    private void CheckContactWithWaterGrid() {
        Collider2D[] contacts = new Collider2D[10];
        int contactCount = Physics2D.OverlapCollider(blockCollider, new ContactFilter2D(), contacts);

        for (int i = 0; i < contactCount; i++) {
            Collider2D contact = contacts[i];

            if (contact.CompareTag("Water")) {
                Water waterGrid = contact.GetComponent<Water>();
                if (waterGrid != null) {
                    // Notifica o Water para congelar o tile na posição do IceBlock
                    Vector3Int gridPosition = waterGrid.GetComponent<Tilemap>().WorldToCell(transform.position);
                    waterGrid.FreezeTile(gridPosition);
                }
            }
        }
    }

    private void Update() {
        CheckContactWithWaterGrid();
    }

    private void Melt() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer") || collision.gameObject.CompareTag("Block")) {
            ColorAttr otherColor = collision.gameObject.GetComponent<Player>()?.colorAttr ?? collision.gameObject.GetComponent<Block>()?.colorAttr;

            if (otherColor?.colorTemperature == ColorTemperature.Warm) {
                Melt();
            }
        }
    }
}