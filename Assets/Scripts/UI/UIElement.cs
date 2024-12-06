using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour {
    public static bool isOpen = false;
    public bool shouldClose { get; set; } = true;

    protected virtual void Start() {
        
    }

    public virtual void ToggleVisibility() {
        if (shouldClose) {
            gameObject.SetActive(isOpen);
            
            isOpen = !isOpen;
        }
    }

    public virtual void SelfDestroy() {
        Destroy(gameObject);
    }
}