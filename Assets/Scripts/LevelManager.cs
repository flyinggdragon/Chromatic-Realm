using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager: MonoBehaviour {
    public Level level;
    private void Start() {
        GameManager.chromaticCircleUses = level.cccUses;
        AudioManager.Instance.PlayMusic(level.levelMusic);
    }

    public IEnumerator End() {
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        level.completed = true;

        yield return StartCoroutine(uiManager.DisplayVictory());
        SceneManager.LoadScene("Level Select");
    }
}