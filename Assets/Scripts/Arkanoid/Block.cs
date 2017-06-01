using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid {

public class Block : MonoBehaviour {

    public string color = "Blue";

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        gameObject.GetComponent<Animator>().SetTrigger(color);
    }
}

}
