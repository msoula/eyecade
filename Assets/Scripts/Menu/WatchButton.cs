using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchButton : MonoBehaviour
{

    public WatchableGame game;
    public UnityEngine.UI.Button button;
    public bool shouldExit = false;

    private Collider2D _collider;
    private float _pauseTimer;
    private bool  _gazed;

    void Start() {
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!_gazed)
        {
            if (game.IsGazed(_collider))
            {
                _gazed = true;
                _pauseTimer = 1f;
                return;
            }
        }
        else
        {
            if (game.IsGazed(_collider))
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
