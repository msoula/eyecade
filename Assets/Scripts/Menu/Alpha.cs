using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : MonoBehaviour {

    public void change_alpha_to(float color)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = color;
        GetComponent<SpriteRenderer>().color = tmp;
    }
}
