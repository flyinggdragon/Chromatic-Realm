using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    [SerializeField] private AudioClip levelMusic;
    //[SerializeField] private GameObject forest;

    private void Start() {
        AudioManager.Instance.PlayMusic(levelMusic);
    }

    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextWorld() {
        
    }

    public void PreviousWorld() {
        
    }
}