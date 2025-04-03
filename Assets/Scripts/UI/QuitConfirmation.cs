using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class QuitConfirmation : UIElement {
    protected override void Start() {
        shouldClose = false;
        UIManager.uiOpen = true;
        Player.shouldMove = false;
        Player.shouldInput = false;
    }

    public void Yes() {
        SelfDestroy();
        UIManager.uiOpen = false;
        Player.shouldMove = true;
        Player.shouldInput = true;
        SceneManager.LoadScene("Level Select");
    }

    public void No() {
        SelfDestroy();
        UIManager.uiOpen = false;
        Player.shouldMove = true;
        Player.shouldInput = true;
        FindFirstObjectByType<UIManager>().TogglePause();
    }
}