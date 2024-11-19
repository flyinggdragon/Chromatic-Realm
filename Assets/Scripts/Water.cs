using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : MonoBehaviour {
    private Tilemap waterTilemap;
    [SerializeField] private Tile iceTile; // Tile de gelo.
    [SerializeField] private AnimatedTile waterTile; // Animated Tile de água.

    private void Start() {
        // Obter referência ao Tilemap.
        waterTilemap = GetComponent<Tilemap>();
    }

    public void FreezeTile(Vector3Int position) {
        // Verifica se o tile já não é de gelo para evitar redundância.
        if (waterTilemap.GetTile(position) != iceTile) {
            waterTilemap.SetTile(position, iceTile);
        }
    }

    public void MeltTile(Vector3Int position) {
        // Verifica se o tile já não é de água para evitar redundância.
        if (waterTilemap.GetTile(position) != waterTile) {
            waterTilemap.SetTile(position, waterTile);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Acessa o collider do objeto com o qual ocorreu a colisão.
        Collider2D other = collision.collider;

        // Calcula a posição do tile exata do contato.
        Vector3 contactPosition = collision.GetContact(0).point; // Usa o ponto exato de contato.
        Vector3Int gridPosition = waterTilemap.WorldToCell(contactPosition); // Converte para posição da grade.

        // Congela o tile se for um IceBlock.
        if (other.CompareTag("IceBlock")) {
            Debug.Log("IceBlock collision");
            FreezeTile(gridPosition);
        }

        // Derrete ou congela com base na cor do jogador ou de outros blocos.
        if (other.CompareTag("ThePlayer") || other.CompareTag("Block")) {
            ColorAttr otherColor = collision.gameObject.GetComponent<Player>()?.colorAttr ?? collision.gameObject.GetComponent<Block>()?.colorAttr;

            if (otherColor?.colorTemperature == ColorTemperature.Warm) {
                Debug.Log("Player is warm");
                MeltTile(gridPosition);
            } else if (otherColor?.colorTemperature == ColorTemperature.Cool) {
                Debug.Log("Player is cool");
                FreezeTile(gridPosition);
            }
        }
    }
}