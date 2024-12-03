using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
    public string levelName;

    
    [Header("Level Attributes")]
    public bool locked;
    public bool completed;
    
    public void LoadStage() {
        SceneManager.LoadScene(levelName);
    }

}