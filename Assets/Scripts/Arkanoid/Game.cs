using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private static float BLOCKS_ORIG_X = -95f;
    private static float BLOCKS_ORIG_Y = 75f;
    private static float BLOCK_HORIZONTAL_GAP = 17f;
    private static float BLOCK_VERTICAL_GAP = 10f;
    private static int BLOCKS_PER_LINE = 12;

    private static float LIVES_ORIG_X = -90f;
    private static float LIVES_ORIG_Y = 108f;
    private static float LIVE_HORIZONTAL_GAP = 10f;
    private static int LIVES_MAX = 3;

    public GameObject tryProto;
    private GameObject[] tries;
    private int triesCount = 0;

    public Arkanoid.Ball ball;
    public Arkanoid.Racket racket;
    public Score score;

    public GameObject[] blockProtos;
    private GameObject[] blocks;
    private int blockCount;

	// Use this for initialization
	void Start () {

        tries = new GameObject[LIVES_MAX];
        blocks = new GameObject[BLOCKS_PER_LINE*blockProtos.Length];

        OnReset(true);

	}

    void OnReset(bool resetScore) {

        int line = 0;
        blockCount = 0;
        foreach (GameObject proto in blockProtos) {
            for (int i = 0; i < BLOCKS_PER_LINE; ++i) {
                if (null == blocks[blockCount]) {
                    blocks[blockCount] = Instantiate(proto, new Vector2(BLOCKS_ORIG_X + (i*BLOCK_HORIZONTAL_GAP), BLOCKS_ORIG_Y - (line*BLOCK_VERTICAL_GAP)), Quaternion.identity);
                    blocks[blockCount].GetComponent<Arkanoid.Block>().game = GetComponent<WatchableGame>();
                }
                blockCount++;
            }
            line++;
        }

        triesCount = 0;
        for (int i = 0; i < LIVES_MAX; ++i) {
            tries[triesCount++] = Instantiate(tryProto, new Vector2(LIVES_ORIG_X + (i*LIVE_HORIZONTAL_GAP), LIVES_ORIG_Y), Quaternion.identity);
        }

        if (resetScore) {
            score.GetComponent<Score>().OnReset();
        }

    }

    void CheckHit() {

        for(int i = 0; i < BLOCKS_PER_LINE * blockProtos.Length; ++i) {

            if (null == blocks[i]) continue;

            if (!blocks[i].GetComponent<Arkanoid.Block>().alive) {
                score.OnScoreInc(blocks[i].GetComponent<Arkanoid.Block>().gain);
                blocks[i].GetComponent<Arkanoid.Block>().OnDie();
                blocks[i] = null;
                blockCount--;
            }

        }
    }

    void CheckHealth() {
        if (ball.IsDead()) {
            Destroy(tries[--triesCount].gameObject);
            ball.OnReset();
        }
    }

    void Update() {
        CheckHit();
        if (0 == blockCount) {
            OnReset(false);
            return;
        }

        CheckHealth();
        if (0 == triesCount) {
            OnReset(true);
        }
    }



}
