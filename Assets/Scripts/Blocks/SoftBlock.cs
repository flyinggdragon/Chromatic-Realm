using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlock : Block {
    private float GetJumpHeight(ColorAttr playerColor) {
        ColorAttr currentColorAttr = ChrColor.FindColorAttr(currentColorName);

        Harmony currentHarmony = ChrColor.DetermineHarmony(currentColorAttr, playerColor);
        
        switch(currentHarmony) {
            case Harmony.All:
                return 10f;

            case Harmony.Complementary:
                return 10f;
            
            case Harmony.Analogue:
                return 7f;
            
            case Harmony.Triadic:
                return 5f;
            
            case Harmony.Equal:
                return 0f;
            
            default:
                return 2.5f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        if (IsTopCollision(collision.contacts)) {
            Player player = collision.gameObject.GetComponent<Player>();

            // Calcula a altura do pulo com base na cor
            float jumpForce = GetJumpHeight(player.colorAttr);
            player.Jump(jumpForce);
        }
    }
}