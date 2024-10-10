using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour, IUIElement {
    public GameObject obj;
    public bool currentlyActive { get; set; }

    protected virtual void Start() {
        
    }

    public virtual void ToggleVisibility() {
        obj.SetActive(!currentlyActive);
    }
}