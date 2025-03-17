using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConnectedBlock : Block {
    [SerializeField] public ConnectedBlock connectedBlock;
    [SerializeField] public ConnectionType connectionType;
    [SerializeField] public LineRenderer lineRenderer;
    
    protected override void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        // Atualiza a cor para a cor escolhida no Inspector.
        colorAttr = ChrColor.FindColorAttr(currentColorName);

        currentColorName = colorAttr.chrColorName;
        color = colorAttr.rgbValue;
        sr.color = color;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(1f, 1f, 1f, 0.5f);
        lineRenderer.endColor = new Color(1f, 1f, 1f, 0.5f);
        lineRenderer.positionCount = 2;
    }

    protected void Update() {
        if (connectedBlock is not null) {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, connectedBlock.transform.position);
        }

        UpdateConnectingLineColor();
    }

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
        block.colorAttr = colorAttr;
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

    private void UpdateConnectingLineColor() {
        Color thisColor = colorAttr.rgbValue;
        Color connectedColor = connectedBlock.colorAttr.rgbValue;

        if (connectionType is ConnectionType.Contrast) {
            lineRenderer.startColor = new Color(thisColor.r, thisColor.g, thisColor.b, 0.5f);
            lineRenderer.endColor = new Color(connectedColor.r, connectedColor.g, connectedColor.b, 0.5f);

            Debug.Log(thisColor);
            Debug.Log(connectedColor);
            Debug.Log(lineRenderer.startColor);
            Debug.Log(lineRenderer.endColor);
        } else {            
            lineRenderer.startColor = new Color(thisColor.r, thisColor.g, thisColor.b, 0.5f);
            lineRenderer.endColor = new Color(thisColor.r, thisColor.g, thisColor.b, 0.5f);
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