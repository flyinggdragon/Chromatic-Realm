using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    private AudioClip currentMusic;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip, float volume = 0.5f) {
        if (currentMusic == clip) return;

        currentMusic = clip;
        
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Stop();
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 0.5f) {
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }
}