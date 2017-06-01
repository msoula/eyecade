using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchButton : MonoBehaviour
{

    public float _distance;
    private Eyex _eye;
    private float _pauseTimer;
    private bool eyeOver;
    public bool shouldExit = false;

    void Start()
    {
        GameObject go = GameObject.Find("eye");
        _eye = (Eyex)go.GetComponent(typeof(Eyex));
    }

    bool isNear(Vector3 v3Pos, float distance)
    {
        v3Pos = v3Pos - transform.position;
        return ((v3Pos.x > -distance && v3Pos.y > -distance) && (v3Pos.x < distance && v3Pos.y < distance));
    }

    void Update()
    {
        if (!eyeOver)
        {
            if (isNear(_eye.transform.position, _distance))
            {
                eyeOver = true;
                _pauseTimer = 5f;
            }
        }
        else
        {
            if (isNear(_eye.transform.position, _distance))
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