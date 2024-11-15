using UnityEngine;

public class Parallax : MonoBehaviour {
    private float startPosX, length;
    public GameObject cam;
    public float parallaxEffect;

    private void Start() {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate() {
        float distanceX = cam.transform.position.x * parallaxEffect;
        float movementX = cam.transform.position.x * (1 - parallaxEffect);

        // Atualiza a posição do fundo
        transform.position = new Vector3(startPosX + distanceX, cam.transform.position.y -4, transform.position.z);

        // Lógica de repetição infinita no eixo X
        if (movementX > startPosX + length) {
            startPosX += length;
        } else if (movementX < startPosX - length) {
            startPosX -= length;
        }
    }
}