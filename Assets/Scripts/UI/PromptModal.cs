using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PromptModal: MonoBehaviour {
    public void OnConfirm() {
        Debug.Log("Confirm");
    }

    public void OnCancel() {
        Debug.Log("Cancel");
    }
}