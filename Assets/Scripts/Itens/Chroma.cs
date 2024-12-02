using UnityEngine;

public class Chroma : Item {
    public AudioClip victorySound;
    protected override void Collect() {
        AudioManager.Instance.PlaySFX(victorySound, 1.0f);

        StartCoroutine(GameObject.FindFirstObjectByType<Level>().End());
    }
}