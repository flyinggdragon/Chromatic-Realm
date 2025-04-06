using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject {
    public Level next;
    public string levelName;
    public AudioClip levelMusic;
    public bool available;
    public bool completed;
    public int cccUses;

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    
    public void LoadLevel() {
        GameManager.levelChromaticCircleUses = cccUses;
        GameManager.chromaticCircleUses = cccUses;
        AudioManager.Instance.PlayMusic(levelMusic);
        SceneManager.LoadScene(levelName);
    }

    public void Complete() {
        completed = true;

        if (next is not null) next.available = true;
    }
}