using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConnectedBlock : Block {
    [SerializeField] public ConnectedBlock connectedBlock;
    [SerializeField] public ConnectionType connectionType;

    public override void ObjectAttrVisualColorChange(ColorAttr newColorAttr) {
        if (connectionType == ConnectionType.Contrast) {
            colorAttr = ChrColor.FindColorAttr(newColorAttr.chrColorName);
            
            ColorAttr complementaryColorAttr = ChrColor.FindColorAttr(newColorAttr.complementaryColor);
            ApplyColorToBlock(connectedBlock, complementaryColorAttr);
        } 
        else if (connectionType == ConnectionType.Equality) {
            colorAttr = ChrColor.FindColorAttr(newColorAttr.chrColorName);
            ApplyColorToBlock(connectedBlock, colorAttr);
        }

        ApplyColorToBlock(this, colorAttr);
    }

    private void ApplyColorToBlock(Block block, ColorAttr colorAttr) {
        block.currentColorName = colorAttr.chrColorName;
        block.color = colorAttr.rgbValue;
        block.sr.color = colorAttr.rgbValue;

        foreach (Block subBlock in block.GetComponents<Block>()) {
            if (subBlock != block) {
                subBlock.currentColorName = colorAttr.chrColorName;
                subBlock.color = colorAttr.rgbValue;
                subBlock.sr.color = colorAttr.rgbValue;
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer") && IsTopCollision(collision.contacts)) {
            collision.gameObject.GetComponent<Player>().grounded = true;
        }
    }

    [System.Serializable]
    public enum ConnectionType {
        Contrast,
        Equality
    }
}