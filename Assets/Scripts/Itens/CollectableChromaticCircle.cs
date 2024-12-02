using System;
using System.Collections.Generic;
using UnityEngine;
public class CollectableChromaticCircle : Item {
    protected override void Collect() {
        if (GameManager.chromaticCircleUses >= 0) {
            GameManager.chromaticCircleUses += 1;
        }

        SelfDestroy();
    }
}