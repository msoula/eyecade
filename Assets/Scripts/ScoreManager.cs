using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public int score;
    public TextMesh text;

    void Start()
    {
       text = GetComponent<TextMesh>();
       score = 0;
    }

    public void Increment()
    {
        score += 1;
    }

    void Update()
    {
        text.text = score.ToString();
    }
}
