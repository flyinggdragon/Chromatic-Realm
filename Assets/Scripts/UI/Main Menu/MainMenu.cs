using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject color;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;
    private GameObject currentView;

    private void Start() {
        currentView = main;

        AudioManager.Instance.PlayMusic(backgroundMusic);
    }

    public void GameStart() {
        SceneManager.LoadScene("Level Select");
    }

    public void DisplayOptions() {
        SwitchView(options);
    }

    public void DisplayColorInformation() {
        SwitchView(color);
    }

    public void DisplayCredits() {
        SwitchView(credits);
    }

    public void DisplayMain() {
        SwitchView(main);
    }

    public void GameQuit() {
        Application.Quit();
    }

    private void SwitchView(GameObject newView) {
        currentView.SetActive(false);

        currentView = newView;
        currentView.SetActive(true);
    }
}