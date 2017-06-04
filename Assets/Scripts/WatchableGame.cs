using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchableGame : MonoBehaviour {

    public SpriteRenderer eye;

    public bool IsGazed(Vector3 pos) {
        return eye.bounds.Contains(pos);
    }

    public bool IsGazed(Bounds bounds) {
        return bounds.Contains(eye.bounds.center);
    }

}
