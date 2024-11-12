using System;
using System.Collections.Generic;
using UnityEngine;
public class CollectableChromaticCircle : MonoBehaviour, ICollectable {
    public void Collect() {
        if (GameManager.chromaticCircleUses >= 0) {
            GameManager.chromaticCircleUses += 1;
        }

        SelfDestroy();
    }
    
    public void SelfDestroy() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            Collect();
        }
    }
}