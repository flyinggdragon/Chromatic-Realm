using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Slider music;
    public Slider sfx;

    public void Start() {
        music.value = AudioManager.Instance.musicSource.volume;
        sfx.value = AudioManager.Instance.sfxSource.volume;
    }

    public void Update() {
        AudioManager.Instance.musicSource.volume = music.value;
        AudioManager.Instance.sfxSource.volume = sfx.value;
    }
}