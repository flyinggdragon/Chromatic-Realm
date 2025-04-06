using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chroma : Item {
    public AudioClip victorySound;
    public Level level;

    protected override void Collect() {
        AudioManager.Instance.PlaySFX(victorySound);
        UIManager.uiOpen = true;
        Player.shouldMove = false;
        Player.shouldInput = false;

        StartCoroutine(EndStage());
    }

    private IEnumerator EndStage() {
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        level.Complete();

        yield return StartCoroutine(uiManager.DisplayVictory());
        GameManager.chromaticCircleUses = 0;
        
        UIManager.uiOpen = false;
        SceneManager.LoadScene("Level Select");
    }
}