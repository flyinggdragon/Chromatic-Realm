using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public void Toggle() {
        if (!GameManager.isPaused) {
            GameObject.FindAnyObjectByType<Player>().ResetMovement();
            GameManager.DisableMovement();
        }
        else {
            GameManager.EnableMovement();
        }
        
        gameObject.SetActive(!GameManager.isPaused);
        GameManager.isPaused = !GameManager.isPaused;

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