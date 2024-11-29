using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour {
    public bool currentlyActive { get; set; } = false;
    public bool shouldClose { get; set; } = true;

    protected virtual void Start() {
        
    }

    public virtual void ToggleVisibility() {
        currentlyActive = !currentlyActive;
        if (shouldClose) gameObject.SetActive(currentlyActive);
    }

    public virtual void SelfDestroy() {
        Destroy(gameObject);
    }
}