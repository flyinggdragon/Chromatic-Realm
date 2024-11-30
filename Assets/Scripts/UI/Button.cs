using UnityEngine;

public class Button : MonoBehaviour {
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip hoverClip;
    public void HoverSound() {
        AudioManager.Instance.PlaySFX(hoverClip);
    }

    public void ClickSound() {
        AudioManager.Instance.PlaySFX(clickClip);
    }
}