using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    public static int RSPEED_MAX = 90;

    public float rSpeed = 0;
    public float incRSpeed = 2;
    public float decRSpeed = 0.7f;
    public float hitValue = 100f;

    private bool _eyeOver = false;
    private Eyex _eyeTracker;
    private CircleCollider2D _collider;
    private BoxCollider2D _boxCollider;

    void Start() {

        _collider = GetComponent<CircleCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        GameObject eyex = GameObject.Find("eye");
        _eyeTracker = eyex.GetComponent<Eyex>();
    }

    public bool IsHitting(GameObject obj) {
        return _boxCollider.bounds.Contains(obj.transform.position);
    }

    bool IsTouching() {
        float radius = _collider.radius;
        return radius >= Vector3.Distance(transform.position, _eyeTracker.transform.position);
    }

	// Update is called once per frame
	void Update () {

        _eyeOver = IsTouching();

        if (_eyeOver && rSpeed < RSPEED_MAX) {
            rSpeed += incRSpeed;
            if (RSPEED_MAX < rSpeed) {
                rSpeed = RSPEED_MAX;
            }
        } else if (!_eyeOver && 0 < rSpeed) {
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
        return 0 < rSpeed;
    }

    public bool IsAccelerating() {
        return _eyeOver && rSpeed < RSPEED_MAX;
    }

    public bool IsDecelerating() {
        return !_eyeOver && 0 < rSpeed;
    }

    public bool IsMax() {
        return _eyeOver && rSpeed == RSPEED_MAX;
    }
}
