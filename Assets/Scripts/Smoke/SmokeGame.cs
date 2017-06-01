using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGame : MonoBehaviour {

    public static int ENEMIES_MAX = 100;

    public static float ENEMY_POP_MAX_DELAY = 5f;
    public static float ENEMY_POP_MIN_DELAY = 0.5f;

    public GameObject enemyPrototype;
    private GameObject[] enemies = new GameObject[ENEMIES_MAX];
    private int enemyCount = 0;

    public GameObject[] spinners;
    private float _lastPopTimer;

    public GameObject background;
    private Color _backgroundColor;

	// Use this for initialization
	void Start () {
        _backgroundColor = background.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < ENEMIES_MAX; ++i) {
            enemies[i] = null;
        }
	}

    void CheckHit() {

        for (int i = 0; i < ENEMIES_MAX; ++i) {

            GameObject enemy = enemies[i];
            if (null == enemy) { continue; }

            foreach(GameObject spinner in spinners) {

                Spinner spinBehavior = spinner.GetComponent<Spinner>();
                if (spinBehavior.IsSpinning() && spinBehavior.IsHitting(enemy)) {

                    if (enemy.GetComponent<SmokeEnemy>().Hit(spinBehavior.hitValue)) {

                        // enemy is dead
                        enemy.GetComponent<Destroyable>().DestroyMe();
                        enemyCount--;
                        enemies[i] = null;
                    }
                }

            }

        }

    }

    void CheckHealth() {

        float health = (float) enemyCount / ENEMIES_MAX;

        Color color = new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 1f - Mathf.Max(0.2f, health));
        for (int i = 0; i < background.transform.childCount; ++i) {
            background.transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }

    }

	// Update is called once per frame
	void Update () {

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
                    break;
                }
            }
            _lastPopTimer = ENEMY_POP_MIN_DELAY;
        }

	}
}
