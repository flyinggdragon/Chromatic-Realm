using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {
    public int cccUses;
    public AudioClip levelMusic;

    private void Start() {
        GameManager.chromaticCircleUses = cccUses;
        AudioManager.Instance.PlayMusic(levelMusic);
    }
}