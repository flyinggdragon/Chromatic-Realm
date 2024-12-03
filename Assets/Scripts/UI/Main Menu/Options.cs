using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Slider music;
    public Slider sfx;

    public void Update() {
        AudioManager.Instance.musicSource.volume = music.value;
        AudioManager.Instance.sfxSource.volume = sfx.value;
    }
}