using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OmnichromaticBlock : Block {
    // De quantos em quantos degundos a cor troca.
    private int timerSeconds = 3;

    protected override void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        ColorUpdate(ChrColor.FindColorAttr(currentColorName));
        // (Até aqui idêntico à classe-pai.)

        StartCoroutine(TimerController(timerSeconds));
    }

    private IEnumerator TimerController(int timerSeconds) {
        // Pega o attr da cor do Inspector e faz a iteração começar pelo índice dela.
        int i = ChrColor.colors.IndexOf(ChrColor.FindColorAttr(currentColorName));

        if (i > 11) {
            Debug.LogWarning($"Cor {currentColorName} inválida no objeto {gameObject.name} no Inspector pois está out of bounds da lista de cores. Definindo a cor {ChrColor.Red.chrColorName} como padrão.");

            i = 0;
        } 

        while (true) {
            // Troca a cor para a próxima da lista após timerSeconds segundos.
            if (i >= (ChrColor.colors.Count - 2)) i = 0; 
             
            ColorUpdate(ChrColor.colors[i]);
            i++;

            yield return new WaitForSeconds(timerSeconds);
        }
    }
}