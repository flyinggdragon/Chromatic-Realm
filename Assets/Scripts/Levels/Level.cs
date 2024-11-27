using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {
    public int cccUses = -1;

    private void Start() {
        GameManager.chromaticCircleUses = cccUses;
    }
}