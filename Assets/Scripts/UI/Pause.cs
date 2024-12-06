using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public static bool isPaused = false;

    public void Toggle() {
        GameManager.ResetMovement();
    
        if (!Textbox.isOpen) {
            isPaused = !isPaused;

            if (isPaused) Time.timeScale = 0f;
            else Time.timeScale = 1f;
        }
        
        gameObject.SetActive(isPaused);
    }

    public void OnReturn() {
        Toggle();
    }

    public void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Toggle();
    }

    public void OnQuit() {
        SceneManager.LoadScene("Main Menu");
        Toggle();
    }
}