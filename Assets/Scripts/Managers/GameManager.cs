using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static int levelChromaticCircleUses;
    public static int chromaticCircleUses;

    public static void ResetMovement() {
        GameObject.FindFirstObjectByType<Player>().ResetMovement();
    }
}