using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour {
    [Header("Level Configuration")]
    public string levelName;
    public int cccUses;
    public AudioClip levelMusic;

    
    [Header("Level Attributes")]
    public bool locked;
    public bool completed;

    private void Start() {
        GameManager.chromaticCircleUses = cccUses;
        AudioManager.Instance.PlayMusic(levelMusic);
        GameManager.EnableMovement();
    }

    public void Load() {
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator End() {
        // Iniciar sequência de vitória.
        GameManager.DisableMovement();

        UIManager uiManager = FindFirstObjectByType<UIManager>();

        completed = true;

        yield return StartCoroutine(uiManager.DisplayVictory());
        SceneManager.LoadScene("Level Select");
    }
}