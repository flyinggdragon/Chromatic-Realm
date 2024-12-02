using UnityEngine;

public class Item : MonoBehaviour {
    protected virtual void Collect() {
        SelfDestroy();
    }
    
    protected virtual void SelfDestroy() {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            Collect();
        }
    }
}