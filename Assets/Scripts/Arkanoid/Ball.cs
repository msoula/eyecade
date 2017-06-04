using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Ball : MonoBehaviour {

    private static float ORIG_Y = -75f;

    public WatchableGame game;
    public GameObject racket;
    public float speed = 100f;

	// Use this for initialization
	void Start () {
        OnReset();
	}

    public void OnReset() {
        transform.position = new Vector2(racket.transform.position.x, ORIG_Y);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    public bool IsDead() {
        return transform.position.y < -120f;
    }

	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Hit the Racket?
        if (col.gameObject.name == "racket") {
            // Calculate hit Factor
            float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }
}

}
