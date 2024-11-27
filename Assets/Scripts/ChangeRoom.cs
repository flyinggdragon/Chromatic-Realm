using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour {
    [SerializeField] private ColorInterface colorInterface;
    [SerializeField] private BoxCollider2D activationTrigger;

    private void ChangeColor(Player player) {
        GameManager.DisableMovement();
        
        StartCoroutine(colorInterface.AsyncColorSelection());
        // Decrementar um de CCC (futuro)
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("ThePlayer")) {
            ChangeColor(other.GetComponent<Player>());
        }
    }
}