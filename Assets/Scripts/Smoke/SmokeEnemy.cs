using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnemy : MonoBehaviour {

    public static float DEFAULT_RECOVER_TIME = 0.033f;

    public float life = 1000;

    private float _recoverTimer = 0f;

    public bool Hit(float hit) {

        if (0 < _recoverTimer) {
            return false;
        }

        life -= hit;

        _recoverTimer = DEFAULT_RECOVER_TIME;

        bool isDead = false;
        if (0 >= life) {
            isDead = true;
        }

        return isDead;
    }

    void Update() {
        if (0 < _recoverTimer) {
            _recoverTimer -= Time.deltaTime;
            return;
        }
    }
}
