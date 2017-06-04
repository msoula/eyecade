using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public static float SPEED_MAX = 60;
    public static float SPEED_MIN = 30;

    public WatchableGame game;
    public float speed = SPEED_MIN;

    private Collider2D _collider;
    public GameObject overlayLeft;
    private Bounds overlayBoundsLeft;
    public GameObject overlayRight;
    private Bounds overlayBoundsRight;

    private SpriteRenderer _renderer;

    public Score scoreLeft;
    public Score scoreRight;

	void Start () {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

        overlayBoundsLeft = new Bounds(overlayLeft.transform.position, new Vector3(30f, 70f, 0f));
        overlayBoundsRight = new Bounds(overlayRight.transform.position, new Vector3(30f, 70f, 0f));
	}

    float HitFactor(Vector2 Ball_Pos, Vector2 Racket_Pos, float Racket_Height) {
        return ((Ball_Pos.y - Racket_Pos.y) / Racket_Height);
    }

    void Update() {

        bool hidden = (overlayBoundsLeft.Contains(_collider.bounds.center) || overlayBoundsRight.Contains(_collider.bounds.center)) && !game.IsGazed(_renderer.bounds.center);
        _renderer.enabled = !hidden;
        if (hidden) {
            GetComponent<TrailRenderer>().Clear();
        }

    }

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.name == "racket_left")
        {
            speed = Mathf.Min(speed + 7f, SPEED_MAX);

            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            GetComponent<TrailRenderer>().material.SetColor("_EmissionColor", new Color(0f, 240f, 210f, 1f));
        }
        if (col.gameObject.name == "racket_right")
        {
            speed = Mathf.Min(speed + 7f, SPEED_MAX);

            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(-1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            GetComponent<TrailRenderer>().material.SetColor("_EmissionColor", new Color(255f, 0f, 100f, 1f));
        }
        if (col.gameObject.name == "wall_right") {
            GetComponent<Rigidbody2D>().position = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            GetComponent<TrailRenderer>().Clear();

            scoreLeft.OnScoreInc(1);

            speed = SPEED_MIN;

            Camera.main.GetComponent<CamShakeSimple>().OnShakeOnCollision(col, .025f);
        }
        if (col.gameObject.name == "wall_left") {
            GetComponent<Rigidbody2D>().position = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            GetComponent<TrailRenderer>().Clear();

            scoreRight.OnScoreInc(1);

            speed = SPEED_MIN;

            Camera.main.GetComponent<CamShakeSimple>().OnShakeOnCollision(col, .025f);
        }
    }

}
