using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Block : MonoBehaviour {

    public WatchableGame game;
    public float gain = 1;
    public string color = "Blue";
    public bool alive = true;

    private Collider2D _collider2D;

    void Start() {
        _collider2D = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (!game.IsWatched(_collider2D.bounds)) {
            Camera.main.GetComponent<CamShakeSimple>().OnShakeOnCollision(collisionInfo, 0.003f);
            alive = false;
        }
    }

    public void OnDie() {
        gameObject.GetComponent<Animator>().SetTrigger(color);
    }
}

}
