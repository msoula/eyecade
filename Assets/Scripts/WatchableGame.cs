using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchableGame : MonoBehaviour {

    public SpriteRenderer eye;

    public bool IsGazed(SpriteRenderer renderer) {
        return eye.bounds.Intersects(renderer.bounds);
    }
    public bool IsGazed(Collider2D collider) {
        return eye.bounds.Intersects(collider.bounds);
    }

}
