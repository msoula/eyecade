using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Block : MonoBehaviour {

    public float gain = 1;
    public string color = "Blue";

    private Score score;

    void Start() {
        GameObject scoreObj = GameObject.Find("score");
        score = scoreObj.GetComponent<Score>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {

        Camera.main.GetComponent<CamShakeSimple>().OnShakeOnCollision(collisionInfo, 0.003f);
        score.OnScoreInc(gain);

        gameObject.GetComponent<Animator>().SetTrigger(color);
    }
}

}
