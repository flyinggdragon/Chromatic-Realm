using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer: MonoBehaviour {
    private TMP_Text timerText;
    private float elapsedTime;
    private bool isRunning;

    private void Awake() {
        timerText = GetComponent<TMP_Text>();
        StartTimer();
    }

    private void Update() {
        if (isRunning) {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartTimer() {
        isRunning = true;
    }

    public void StopTimer() {
        isRunning = false;
    }

    public void ResetTimer() {
        isRunning = false;
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay() {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt(elapsedTime * 1000 % 1000);

        if (minutes < 10) {
            timerText.text = string.Format("{0}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        } else {
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}