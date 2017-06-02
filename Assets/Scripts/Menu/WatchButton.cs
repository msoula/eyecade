using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchButton : MonoBehaviour
{

    public UnityEngine.UI.Button button;
    public SpriteRenderer eye;
    public bool shouldExit = false;

    private float _pauseTimer;
    private bool  _gazed;

    bool isGazed()
    {
        return eye.bounds.Intersects(GetComponent<Collider2D>().bounds);
    }

    void Update()
    {
        if (!_gazed)
        {
            if (isGazed())
            {
                _gazed = true;
                _pauseTimer = 1f;
                return;
            }
        }
        else
        {
            if (isGazed())
            {

                if (_pauseTimer > 0)
                {
                    _pauseTimer -= Time.deltaTime;
                    return;
                }

            }
            else
            {
                _gazed = false;
                return;
            }
        }

        if (_gazed)
        {
            if (shouldExit)
                Application.Quit();
            button.onClick.Invoke();
        }
    }
}
