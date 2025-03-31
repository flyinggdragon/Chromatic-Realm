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

        currentMusic = musicSource.clip;
    }

    public void PlayMusic(AudioClip clip) {
        if (currentMusic == clip) return;

        currentMusic = clip;
        
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Stop();
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }
}