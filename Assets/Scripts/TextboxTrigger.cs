using System.Collections.Generic;
using UnityEngine;


public class TextboxTrigger : MonoBehaviour {
    public List<string> strings;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("ThePlayer")) {
            GameObject.Find("UI").GetComponent<UIManager>().InstantiateWindow(strings);
            Destroy(gameObject);
        }
    }
}