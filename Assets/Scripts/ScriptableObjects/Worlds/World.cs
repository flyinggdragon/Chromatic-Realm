using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New World", menuName = "World")]
public class World : ScriptableObject {
    public string worldName;
    public List<Level> levels;
    public AudioClip backgroundMusic;
    public LevelSelectBackground background;
}