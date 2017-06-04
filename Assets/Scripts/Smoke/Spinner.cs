using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    public static int RSPEED_MAX = 90;

    public WatchableGame game;
    public float rSpeed = 0;
    public float incRSpeed = 2;
    public float decRSpeed = 0.7f;
    public float hitValue = 100f;
    public Collider2D attackArea;

    private bool _isGazed = false;
    private CircleCollider2D _collider;

    void Start() {
        _collider = GetComponent<CircleCollider2D>();
    }

    public bool IsHitting(Collider2D col) {
        return attackArea.bounds.Intersects(col.bounds);
    }

	// Update is called once per frame
	void Update () {

        _isGazed = game.IsGazed(_collider.bounds);

        if (_isGazed && rSpeed < RSPEED_MAX) {
            rSpeed += incRSpeed;
            if (RSPEED_MAX < rSpeed) {
                rSpeed = RSPEED_MAX;
            }
        } else if (!_isGazed && 0 < rSpeed) {
            rSpeed -= decRSpeed;
            if (0 > rSpeed) {
                rSpeed = 0;
            }
        }

        transform.Rotate(Vector3.forward * -Mathf.Min(rSpeed, RSPEED_MAX));
	}

    public float GetPower() {
        return rSpeed / RSPEED_MAX;
    }

    public bool IsSpinning() {
        return rSpeed > (RSPEED_MAX / 2f);
    }

    public bool IsAccelerating() {
        return _isGazed && rSpeed < RSPEED_MAX;
    }

    public bool IsDecelerating() {
        return !_isGazed && 0 < rSpeed;
    }

    public bool IsMax() {
        return _isGazed && rSpeed == RSPEED_MAX;
    }
}
