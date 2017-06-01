using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchButton : MonoBehaviour
{

    public float _distance;
    public GameObject _eye;
    private float _pauseTimer;
    private bool eyeOver;
    public bool shouldExit = false;
    
    bool isNear(GameObject obj)
    {
        return obj.GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<Collider2D>().bounds);
    }

    void Update()
    {
        if (!eyeOver)
        {
            if (isNear(_eye))
            {
                eyeOver = true;
                _pauseTimer = 0.5f;
            }
        }
        else
        {
            if (isNear(_eye))
            {

                if (_pauseTimer > 0)
                {
                    _pauseTimer -= Time.deltaTime;
                    return;
                }

            }
            else
            {
                eyeOver = false;
                return;
            }
        }
        if (eyeOver)
        {
            if (shouldExit)
                Application.Quit();
            GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }
}