using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Button : MonoBehaviour {
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
        if (level != null) {
            SceneManager.LoadScene(level.levelName);
        } else {
            Debug.LogWarning("O nível não foi atribuído corretamente ao botão!");
        }
    }
}