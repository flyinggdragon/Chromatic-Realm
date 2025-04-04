using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject {
    public string levelName;
    public AudioClip levelMusic;
    public bool available;
    public bool completed = false;
    public int cccUses;

    public void LoadLevel() {
        GameManager.levelChromaticCircleUses = cccUses;
        GameManager.chromaticCircleUses = cccUses;
        AudioManager.Instance.PlayMusic(levelMusic);
        SceneManager.LoadScene(levelName);
    }
}