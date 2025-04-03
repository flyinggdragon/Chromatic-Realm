using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour {
    public bool isOpen = false;
    public bool shouldClose = true;
    [SerializeField] protected AudioClip openSFX;
    [SerializeField] protected AudioClip closeSFX;

    protected virtual void Start() {
        
    }

    protected virtual void Update() {

    }

    public virtual void ToggleVisibility() {
        if (shouldClose) {
            if (UIManager.uiOpen && !isOpen) return;

            isOpen = !isOpen;
            UIManager.uiOpen = isOpen;
            gameObject.SetActive(isOpen);

            Player.shouldMove = !isOpen;
            Player.shouldInput = !isOpen;

            if (isOpen) AudioManager.Instance.PlaySFX(openSFX);
            else AudioManager.Instance.PlaySFX(closeSFX);
        }
    }

    public virtual void SelfDestroy() {
        Destroy(gameObject);
    }
}