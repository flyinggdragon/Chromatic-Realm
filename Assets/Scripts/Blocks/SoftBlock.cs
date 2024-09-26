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

    private void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D[] contacts = collision.contacts;
        
        foreach (ContactPoint2D contact in contacts) {
            // Verifica se o contato foi na parte de cima do bloco
            
            if (contact.normal.y < -0.9f) {
                Player player = collision.gameObject.GetComponent<Player>();

                // Calcula a altura do pulo com base na cor
                float jumpForce = GetJumpHeight(player.colorAttr);
                player.Jump(jumpForce);
            }
        }
    }
}