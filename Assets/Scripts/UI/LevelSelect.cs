using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour {
    [SerializeField] private AudioClip levelMusic;

    // Código incompleto. Implemento isso depois da release da Alpha 1.0.
    //[Header("Level Configuration")]
    //[SerializeField] private List<GameObject> worlds;
    //[SerializeField] private GameObject forest;
    //[SerializeField] private GameObject city;
    //[SerializeField] private GameObject factory;
    //[SerializeField] private GameObject deepwoods;
    //[SerializeField] private GameObject levelPrefab;

    //[Header("Other")]
    //[SerializeField] private GameObject currentlySelected;

    private void Start() {
        AudioManager.Instance.PlayMusic(levelMusic);
        
        /* Código incompleto. Implemento isso depois da release da Alpha 1.0.
        int i = 0;

        foreach (var world in worlds) {
            foreach (var level in world.levels) {
                GameObject levelInstance = Instantiate(levelPrefab, world.transform);
                levelInstance.FindFirstObjectByType<TMP_Text>().text = i;

                levelInstance.transform.parent = world.transform;

                i++;
            }
        }*/
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextWorld() {
        
    }

    public void PreviousWorld() {
        
    }
}