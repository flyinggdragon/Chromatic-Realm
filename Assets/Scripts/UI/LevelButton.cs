using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour {
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] public TMP_Text number;
    public Level level;

    public void HoverSound() {
        AudioManager.Instance.PlaySFX(hoverClip);
    }

    public void ClickSound() {
        AudioManager.Instance.PlaySFX(clickClip);
    }

    public void LoadStage() {
        level.LoadLevel();
    }
}