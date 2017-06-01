using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behind : MonoBehaviour
{

    GameObject go;
    Alpha a;

    void Start()
    {
       go = GameObject.Find("Logo_light");
       a = (Alpha)go.GetComponent(typeof(Alpha));
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("x:" + mousePosition.x + " y:" + mousePosition.y);
        if (mousePosition.x > 31 && mousePosition.x < 339
            && mousePosition.y > 178 && mousePosition.y < 246)
            a.change_alpha_to(255f);
        else
            a.change_alpha_to(0f);
    }
}