using UnityEngine;

public class Chroma : Item {
    protected override void Collect() {
        GameObject.FindFirstObjectByType<Level>().End();

        SelfDestroy();
    }
}