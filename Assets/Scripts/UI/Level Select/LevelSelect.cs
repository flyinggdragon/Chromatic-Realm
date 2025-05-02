using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    private static int worldIndex = 0;
    [SerializeField] private Transform worldsHolder;
    [SerializeField] private List<GameObject> worlds;
    [SerializeField] private List<AudioClip> worldsMusic;
    private GameObject selectedWorld;

    private void Start() {
        selectedWorld = worlds[worldIndex];
        AudioManager.Instance.PlayMusic(worldsMusic[worldIndex]);
        AudioManager.Instance.environmentSource.Stop();

        foreach (GameObject g in worlds) {
            g.SetActive(g == selectedWorld);
        }
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void ScrollWorlds(int increment) {
        int newIndex = worldIndex + increment;

        if (newIndex >= 0 && newIndex < worlds.Count) {
            selectedWorld.SetActive(false);
            worldIndex = newIndex;
            selectedWorld = worlds[worldIndex];

            AudioManager.Instance.PlayMusic(worldsMusic[worldIndex]);
            selectedWorld.SetActive(true);
        }
    }
}