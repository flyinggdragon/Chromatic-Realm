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
            UIManager.uiOpen = isOpen;
            gameObject.SetActive(isOpen);

            if (isOpen) AudioManager.Instance.PlaySFX(openSFX);
            else AudioManager.Instance.PlaySFX(closeSFX);

            Time.timeScale = isOpen ? 0f : 1f;
        }
    }

    public void OnReturn() {
        ToggleVisibility();
    }

    public void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.chromaticCircleUses = GameManager.levelChromaticCircleUses;
        ToggleVisibility();
    }

    public void OnQuit() {
        ToggleVisibility();
        Instantiate(quitWindowPrefab, transform.parent.transform);
    }
}