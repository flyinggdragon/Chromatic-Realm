using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlock : Block {
    private float GetJumpHeight(ColorAttr playerColor) {
        ColorAttr currentColorAttr = ChrColor.FindColorAttr(currentColorName);

        Harmony currentHarmony = ChrColor.DetermineHarmony(currentColorAttr, playerColor);

        switch(currentHarmony) {
            case Harmony.All:
                return 25f;

            case Harmony.Complementary:
                return 25f;
            
            case Harmony.Analogue:
                return 18f;
            
            case Harmony.Triadic:
                return 10f;
            
            case Harmony.Equal:
                return 0f;
            
            default:
            return 5f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            if (IsTopCollision(collision.contacts)) {
                Player player = collision.gameObject.GetComponent<Player>();

                if (!player.isGrabbing) player.SoftBlockJump(GetJumpHeight(player.colorAttr));
            }
        }
    }
}