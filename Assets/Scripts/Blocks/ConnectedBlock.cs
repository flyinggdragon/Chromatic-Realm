using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ConnectedBlock : Block {
    [SerializeField] private ConnectedBlock connectedBlock;
    [SerializeField] private ConnectionType connectionType;
    //private Line connectingLine;
    
    // Temporário
    private int i = 0;

    private void CheckConnectionValidity() {
        // Verifica se eu errei a cor complementar do bloco e me avisa em caso afirmativo
        // Se eu realmente quiser, eu posso programar para se auto-ajustar em caso de erro.
        
        if (connectionType == ConnectionType.Contrast) {
            if (currentColorName != connectedBlock.colorAttr.complementaryColor) {
                Debug.LogWarning($"Conexão de tipo {connectionType} inválida em {gameObject.name}:\n{this.currentColorName} (este bloco) não é complementar a {connectedBlock.currentColorName} (bloco conectado)\nO contraste correto seria: {this.colorAttr.complementaryColor}.");
            }
        }
        
        else if (connectionType == ConnectionType.Equality) { 
            if (currentColorName != connectedBlock.currentColorName) {
                Debug.LogWarning($"Conexão de tipo {connectionType} inválida em {gameObject.name}:\n{this.currentColorName} (este bloco) não é igual a {connectedBlock.currentColorName} (bloco conectado)");
            }
        }

        else {
            Debug.LogError($"Tipo de Conexão desconhecida em {gameObject.name}");
        }
    }

    void Update() {
        CheckConnectionValidity();
    }

    private void UpdateConnectionColors() {
        ChangeColor(ChrColor.colors[i]);

        if (connectionType == ConnectionType.Contrast) {
            connectedBlock.ChangeColor(ChrColor.FindColorAttr(this.colorAttr.complementaryColor));
        } else {
            connectedBlock.ChangeColor(ChrColor.FindColorAttr(currentColorName));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ThePlayer")) {
            // Temporário.
            i++;
            if (i > 10) { i = 0; }
            UpdateConnectionColors();
        }
    }

    [System.Serializable]
    private enum ConnectionType {
        Contrast,
        Equality
    }
}