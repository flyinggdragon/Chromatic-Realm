using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorTheory : MonoBehaviour {
    [SerializeField] private List<Sprite> images;
    private int imgIndex = 0;
    private Sprite selectedImg;
    public static bool playGamePressed;
    public static bool playGamePressedFirstTime = true;
    public GameObject returnButton;
    public GameObject okayButton;

    void Start() {
        selectedImg = images[0];
        returnButton.SetActive(!playGamePressed);
    }

    void Update() {
        GetComponentInChildren<Image>().sprite = selectedImg;

        okayButton.SetActive(playGamePressed && (imgIndex == images.Count - 1));
    }

    public void Scroll(int increment) {
        int newIndex = imgIndex + increment;

        if (newIndex >= 0 && newIndex < images.Count) {
            imgIndex = newIndex;
            selectedImg = images[imgIndex];
        }
    }
    
    public void ReturnToMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void AdvanceToLevelSelect() {
        SceneManager.LoadScene("Level Select");
    }
}