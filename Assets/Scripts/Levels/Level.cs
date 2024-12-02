using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour {
    [Header("Level Configuration")]
    public string levelName;
    public int cccUses;
    public AudioClip levelMusic;
    public bool locked;

    private void Start() {
        GameManager.chromaticCircleUses = cccUses;
        AudioManager.Instance.PlayMusic(levelMusic);
    }

    public void Load() {
        SceneManager.LoadScene(levelName);
    }
}