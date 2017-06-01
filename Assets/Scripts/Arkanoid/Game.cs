using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private static float HORIZONTAL_GAP = 17f;
    private static float VERTICAL_GAP = 10f;

    public GameObject[] blocks;

	// Use this for initialization
	void Start () {

        float origX = -95f, origY = 75f;
        int line = 0;
        foreach (GameObject block in blocks) {
            for (int i = 0; i < 12; ++i) {
                Instantiate(block, new Vector2(origX + (i*HORIZONTAL_GAP), origY - (line*VERTICAL_GAP)), Quaternion.identity);
            }
            line++;
        }

        for (int i = 0; i < 3; ++i) {
        }

	}

}
