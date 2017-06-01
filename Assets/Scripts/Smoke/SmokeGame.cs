using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGame : MonoBehaviour {

    public static int ENEMIES_MAX = 100;

    public static float ENEMY_POP_MAX_DELAY = 5f;
    public static float ENEMY_POP_MIN_DELAY = 0.1f;

    public GameObject enemyPrototype;
    private GameObject[] enemies = new GameObject[ENEMIES_MAX];
    private int enemyCount = 0;

    public GameObject[] spinners;
    private float _lastPopTimer;

    public GameObject background;
    private Color _backgroundColor;

    public AudioClip deathAudio;

	// Use this for initialization
	void Start () {
        _backgroundColor = background.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < ENEMIES_MAX; ++i) {
            enemies[i] = null;
        }
	}

    void CheckHit() {

        foreach(GameObject spinner in spinners) {

            Spinner spinBehavior = spinner.GetComponent<Spinner>();
            for (int i = 0; i < ENEMIES_MAX; ++i) {

                GameObject enemy = enemies[i];
                if (null == enemy) { continue; }

                if (spinBehavior.IsSpinning() && spinBehavior.IsHitting(enemy.GetComponent<CircleCollider2D>())) {

                    if (enemy.GetComponent<SmokeEnemy>().Hit(spinBehavior.hitValue)) {

                        // enemy is dead
                        enemy.GetComponent<AudioSource>().PlayOneShot(deathAudio, 0.7f);
                        enemy.GetComponent<Destroyable>().DestroyMe();
                        enemyCount--;
                        enemies[i] = null;
                    }
                }

            }

        }

    }

    void CheckHealth() {

        float healthRatio = (float) enemyCount / (float) ENEMIES_MAX;
        healthRatio = Mathf.Max(0.4f, 1f - healthRatio);

        Color color = new Color(_backgroundColor.r * healthRatio, _backgroundColor.g * healthRatio, _backgroundColor.b * healthRatio, 1f);
        for (int i = 0; i < background.transform.childCount; ++i) {
            background.transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }

        if (enemyCount == ENEMIES_MAX) {
            OnGameOver();
        }

    }

    void OnGameOver() {
    }

	// Update is called once per frame
	void Update () {

        if (enemyCount == ENEMIES_MAX) {
            return;
        }

        CheckHit();

        CheckHealth();

        if (0 < _lastPopTimer) {
            _lastPopTimer -= Time.deltaTime;
            return;
        }

        if (ENEMIES_MAX > enemyCount) {
            for (int i = 0; i < ENEMIES_MAX; ++i) {
                if (null == enemies[i]) {
                    enemies[i] = Instantiate(enemyPrototype, new Vector2(Random.Range(-8f, 8f), Random.Range(0f, 2f)), Quaternion.identity);
                    enemyCount++;
                    break;
                }
            }
            _lastPopTimer = ENEMY_POP_MIN_DELAY;
        }

	}
}
