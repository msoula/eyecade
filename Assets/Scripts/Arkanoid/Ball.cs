using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Ball : MonoBehaviour {

    private static float ORIG_Y = -75f;

    public float speed = 100f;
    public float distance;

    private Sprite _sprite;
    private Eyex   _eye;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;

        _sprite = GetComponent<SpriteRenderer>().sprite;

        GameObject go = GameObject.Find("eye");
        _eye = (Eyex)go.GetComponent(typeof(Eyex));
	}

    public void Reset() {

        GameObject racket = GameObject.Find("racket");
        transform.position = new Vector2(racket.transform.position.x, ORIG_Y);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;

    }

    bool isNear(Vector3 v3Pos, float distance)
    {
        v3Pos = v3Pos - transform.position;
        return ((v3Pos.x > -distance && v3Pos.y > -distance) && (v3Pos.x < distance && v3Pos.y < distance));
    }

    bool isNear(float x, float y, float distance) {
        Vector3 v3Pos = new Vector3(x, y, 0);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);

        v3Pos = v3Pos - transform.position;

        return ((v3Pos.x > -distance && v3Pos.y > -distance) && (v3Pos.x < distance && v3Pos.y < distance));
    }

    bool isNear(float x1, float y1, float x2, float y2) {
        Vector3 position = transform.position;
        return ((x1 < position.x && y1 > position.y) && (x2 > position.x && y2 < position.y));
    }

    public bool IsDead() {
    return isNear(-110f, -110f, 110f, -120f);
    }

	// Update is called once per frame
    void Update() {


        if (isNear(_eye.transform.position, distance)) {
            GetComponent<SpriteRenderer>().sprite = _sprite;
        } else if (isNear(-110f, 120f, 110, 120f)) { // top
            GetComponent<SpriteRenderer>().sprite = _sprite;
        } else if (isNear(-110f, -30f, 110f, -110)) { // bottom
            GetComponent<SpriteRenderer>().sprite = _sprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = null;
        }

    }

    public bool IsHitting(GameObject obj) {
        return GetComponent<Collider2D>().bounds.Intersects(obj.GetComponent<Collider2D>().bounds);
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
            float x=hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }
}

}
