using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Block : MonoBehaviour {

    public float gain = 1;
    public string color = "Blue";
    public bool alive = true;

    public Eyex _eye;

	void Start () {

        GameObject go = GameObject.Find("eye");
        _eye = (Eyex)go.GetComponent(typeof(Eyex));
	}

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (!_eye.GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<Collider2D>().bounds)) {
            Camera.main.GetComponent<CamShakeSimple>().OnShakeOnCollision(collisionInfo, 0.003f);
            alive = false;
        }
    }

    public void OnDie() {
        gameObject.GetComponent<Animator>().SetTrigger(color);
    }
}

}
