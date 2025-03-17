using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chroma : Item {
    public AudioClip victorySound;
    public Level level;

    protected override void Collect() {
        AudioManager.Instance.PlaySFX(victorySound);
        Player.shouldMove = false;

        StartCoroutine(EndStage());
    }

    private IEnumerator EndStage() {
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        level.completed = true;

        yield return StartCoroutine(uiManager.DisplayVictory());
        GameManager.chromaticCircleUses = 0;
        SceneManager.LoadScene("Level Select");
    }
}