using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, ICanColorChange {
    // Public
    public Collider2D blockCollider;
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; set; }
    public Rigidbody2D rb { get; protected set; }
    public SpriteRenderer sr { get; protected set; }
    
    // Protected
    protected Vector2 _position;
    public Color32 color { get; set; }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        // Atualiza a cor para a cor escolhida no Inspector.
        colorAttr = ChrColor.FindColorAttr(currentColorName);

        currentColorName = colorAttr.chrColorName;
        color = colorAttr.rgbValue;
        sr.color = color;
    }

    public virtual void ObjectAttrVisualColorChange(ColorAttr newColorAttr) {
        // Atualiza a cor para a cor recebida.
        colorAttr = newColorAttr;

        currentColorName = colorAttr.chrColorName;
        color = colorAttr.rgbValue;
        sr.color = color;
    }

    public virtual void ChangeColor(ColorAttr newColorAttr) {
        ObjectAttrVisualColorChange(newColorAttr);

        // Atualiza também a cor de qualquer outro componente <Block> associado ao mesmo Object.
        foreach (Block block in GetComponents<Block>()) {
            if (block == this) continue;

            block.ObjectAttrVisualColorChange(newColorAttr);
        }
    }

    public bool IsTopCollision(ContactPoint2D[] contacts) {
        foreach (ContactPoint2D contact in contacts) {
            // Verifica se o contato foi na parte de cima do bloco
            
            if (contact.normal.y < -0.9f) {
                return true;
            }
        }

        return false;
    }

    protected virtual void OnTriggerEnter2D(UnityEngine.Collider2D collision) {
        if (collision.gameObject.CompareTag("MixRay")) {
            MixRay mixRay = collision.gameObject.GetComponent<MixRay>();

            mixRay.CauseColorChange(gameObject);
        }
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            Player player = collision.gameObject.GetComponent<Player>();

            if (IsTopCollision(collision.contacts)) {
                player.grounded = true;
            }

            /*
            // Puxabilidade dos blocos pelo player.
            Harmony currentHarmony = ChrColor.DetermineHarmony(colorAttr, player.colorAttr);

            // Se a harmonia não for análoga nem igual, não é puxável.
            if (currentHarmony is not Harmony.Equal && currentHarmony is not Harmony.Analogue) {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            } else {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }*/
        }
        
        /*
        // "Puxabuilidade de blocos por blocos".
        if (collision.gameObject.CompareTag("Block")) {
            Block block = collision.gameObject.GetComponent<Block>();

            Harmony currentHarmony = ChrColor.DetermineHarmony(colorAttr, block.colorAttr);

            // Se a harmonia não for análoga nem igual, não é puxável.
            if (currentHarmony is not Harmony.Equal && currentHarmony is not Harmony.Analogue) {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            } else {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        */
    }

    protected virtual void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            Player player = collision.gameObject.GetComponent<Player>();

            player.grounded = false;
        }
    }
}