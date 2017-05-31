using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Racket : MonoBehaviour {

    public float speed = 150;

	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;
	}
}

}
