using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static int chromaticCircleUses;
    public static bool shouldMove = true;
    public static bool shouldInput = true;
    public static bool isPaused = false;

    public static void EnableMovement() {
        shouldMove = true;
        shouldInput = true;
    }

    public static void DisableMovement() {
        GameObject.FindFirstObjectByType<Player>().ResetMovement();
        shouldMove = false;
        shouldInput = false;
    }
}