using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour {
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (Application.loadedLevelName != "menu")
                Application.LoadLevel("menu");
            else
                Application.Quit();
        }
    }
}
