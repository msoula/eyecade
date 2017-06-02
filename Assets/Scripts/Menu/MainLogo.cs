using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogo : MonoBehaviour {

    public WatchableGame game;
    private SpriteRenderer _renderer;

    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
        _renderer.enabled = game.IsGazed(_renderer);
	}
}
