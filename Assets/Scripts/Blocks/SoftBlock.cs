using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlock : Block {
    private float GetJumpHeight(ColorAttr playerColor) {
        ColorAttr currentColorAttr = ChrColor.FindColorAttr(currentColorName);

        Harmony currentHarmony = ChrColor.DefineHarmony(currentColorAttr, playerColor);
        
        switch(currentHarmony) {
            case Harmony.All:
                return 15f;

            case Harmony.Complementary:
                return 13f;
            
            case Harmony.Analogue:
                return 10f;
            
            case Harmony.Triadic:
                return 7f;
            
            case Harmony.Identical:
                return 0f;
            
            default:
                return 2.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            Player player = collision.gameObject.GetComponent<Player>();

            float jumpForce = GetJumpHeight(player.colorAttr);
            player.Jump(jumpForce);
        }
    }
}