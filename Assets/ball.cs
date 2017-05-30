using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

    public float speed = 30;

	void Start () {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}

    float HitFactor(Vector2 Ball_Pos, Vector2 Racket_Pos, float Racket_Height) {
        return ((Ball_Pos.y - Racket_Pos.y) / Racket_Height);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Racket_left")
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            GetComponent<TrailRenderer>().materials[0].SetColor("_EmissionColor", Color.blue);
        }
        else if (col.gameObject.name == "Racket_right")
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(-1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            GetComponent<TrailRenderer>().materials[0].SetColor("_EmissionColor", Color.red);
        }
    }
}
