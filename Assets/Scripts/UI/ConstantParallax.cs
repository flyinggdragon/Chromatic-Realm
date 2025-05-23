using UnityEngine;

public class ConstantParallax : MonoBehaviour {
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;

    void Start() {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate() {
        cam.transform.position += Vector3.right * 0.5f * Time.deltaTime;

        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}