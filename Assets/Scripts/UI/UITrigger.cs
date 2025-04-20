using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour {
    [SerializeField] private GameObject prefab;
    private GameObject instance;
    [SerializeField] private Transform uiParent;
    [SerializeField] private AudioClip sfx;
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("ThePlayer")) {
            AudioManager.Instance.PlaySFX(sfx);
            instance = Instantiate(prefab, uiParent, false);
            // ==StartCoroutine(prefab.fade.FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("ThePlayer")) {
            AudioManager.Instance.PlaySFX(sfx);
            StartCoroutine(instance.GetComponent<Fade>().FadeOut());
            instance = null;
        }
    }
}