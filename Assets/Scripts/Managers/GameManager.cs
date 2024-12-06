using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static int chromaticCircleUses;
    public static bool shouldMove = true;
    public static bool shouldInput = true;

    public static void ResetMovement() {
        GameObject.FindFirstObjectByType<Player>().ResetMovement();
    }
}