using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour {
    public GameObject levelPrefab;
    public Transform levelHolder;
    public TMP_Text worldName;
    public Transform backgroundHolder;
    private int worldIndex = 0;

    [Header("Level Configuration")]
    [SerializeField] private List<World> worlds;

    [Header("Other")]
    public static World currentlySelectedWorld;

    private void Start() {
        if (currentlySelectedWorld is null) currentlySelectedWorld = worlds[0];
        DisplayLevels(currentlySelectedWorld);
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void DisplayLevels(World world) {
        int i = 1;

        currentlySelectedWorld = world;
        worldName.text = currentlySelectedWorld.worldName;
        
        backgroundHolder.GetChild(0).GetComponent<Image>().sprite = currentlySelectedWorld.background.far;
        backgroundHolder.GetChild(1).GetComponent<Image>().sprite = currentlySelectedWorld.background.mid;
        backgroundHolder.GetChild(2).GetComponent<Image>().sprite = currentlySelectedWorld.background.close;
        
        int x;

        if (levelHolder.childCount > 0) {
            for (x = levelHolder.childCount - 1; x >= 0; x--) {
                Destroy(levelHolder.GetChild(x).gameObject);
            }
        }

        AudioManager.Instance.PlayMusic(currentlySelectedWorld.backgroundMusic);

        foreach (Level level in currentlySelectedWorld.levels) {
            GameObject levelInstance = Instantiate(levelPrefab, levelHolder);
            Button btn = levelInstance.GetComponent<Button>();
            btn.level = level;
            btn.number.text = i.ToString();

            levelInstance.transform.SetParent(levelHolder);

            i++;
        }
    }

    public void ScrollWorlds(int increment) {
        worldIndex += increment;
        if (worldIndex < 0) {
            worldIndex = 0;
            return;
        }
        if (worldIndex > worlds.Count) {
            worldIndex = worlds.Count;
            return;
        }
        
        DisplayLevels(worlds[worldIndex]);
    }
}