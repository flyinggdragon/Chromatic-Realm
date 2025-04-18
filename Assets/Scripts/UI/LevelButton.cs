using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] public Text number;
    private Button button;
    private Image img;
    public Level level;

    private void Start() {
        button = GetComponent<Button>();
        img = GetComponent<Image>();

        button.interactable = level.available;
        if (level.completed) img.color = Color.green;
    }

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