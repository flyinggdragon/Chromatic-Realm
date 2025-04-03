using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Pause : UIElement {
    [SerializeField] private GameObject quitWindowPrefab;
    public override void ToggleVisibility() {
        if (shouldClose) {
            if (UIManager.uiOpen && !isOpen) return;

            GameManager.ResetMovement();

            isOpen = !isOpen;
            gameObject.SetActive(isOpen);

            Time.timeScale = isOpen ? 0f : 1f;
        }
    }

    public void OnReturn() {
        ToggleVisibility();
    }

    public void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ToggleVisibility();
    }

    public void OnQuit() {
        ToggleVisibility();
        Instantiate(quitWindowPrefab, transform.parent.transform);
    }
}