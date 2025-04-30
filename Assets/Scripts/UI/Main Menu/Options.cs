using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Slider music;
    public Slider sfx;
    public Slider environment;

    public void Start() {
        music.value = AudioManager.Instance.musicSource.volume;
        sfx.value = AudioManager.Instance.sfxSource.volume;
        environment.value = AudioManager.Instance.environmentSource.volume * 4;
    }

    public void Update() {
        AudioManager.Instance.musicSource.volume = music.value;
        AudioManager.Instance.sfxSource.volume = sfx.value;
        AudioManager.Instance.environmentSource.volume = environment.value / 4;
    }
}