using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Block : MonoBehaviour {

    public float gain = 1;
    public string color = "Blue";
    public bool alive = true;

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        Camera.main.GetComponent<CamShakeSimple>().OnShakeOnCollision(collisionInfo, 0.003f);
        alive = false;
    }

    public void OnDie() {
        gameObject.GetComponent<Animator>().SetTrigger(color);
    }
}

}
