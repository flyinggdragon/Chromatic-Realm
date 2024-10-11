using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomWall : MonoBehaviour {
    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private Transform wall;
    private float raiseSpeed = 5f;
    private float raiseHeight = 2f;
    private Vector3 initialPos;
    private Coroutine currentCoroutine;

    private void Start() {
        initialPos = wall.position;
    }

    private IEnumerator RaiseWall() {
        Vector3 targetPosition = wall.position + Vector3.up * raiseHeight;

        while (Vector3.Distance(wall.position, targetPosition) > 0.01f) {
            wall.position = Vector3.MoveTowards(wall.position, targetPosition, raiseSpeed * Time.deltaTime);
            yield return null;
        }

        wall.position = targetPosition;
    }

    private IEnumerator LowerWall() {
        Vector3 targetPosition = initialPos;

        while (Vector3.Distance(wall.position, targetPosition) > 0.01f) {
            wall.position = Vector3.MoveTowards(wall.position, targetPosition, raiseSpeed * Time.deltaTime);
            yield return null;
        }

        wall.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ThePlayer")) {
            if (currentCoroutine != null) {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(RaiseWall());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("ThePlayer")) {
            if (currentCoroutine != null) {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(LowerWall());
        }
    }
}