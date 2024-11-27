using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static int chromaticCircleUses;
    public static bool shouldMove = true;
    public static bool shouldInput = true;

    public static void EnableMovement() {
        shouldMove = true;
        shouldInput = true;
    }

    public static void DisableMovement() {
        // Chama o método direto do Player para parar o movimento
        GameObject.FindFirstObjectByType<Player>().ResetMovement();
        shouldMove = false;
        shouldInput = false;
    }
}