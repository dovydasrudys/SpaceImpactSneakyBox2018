using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemyType;
    public int spawnTimeIntervals;
    public int enemiesSpawned;
    float timer = 0f;
	// Use this for initialization
	void Start () {
        transform.Rotate(new Vector3(0, 0, -90));
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= spawnTimeIntervals)
        {
            SpawnEnemy(enemyType);
            timer = 0;
            enemiesSpawned++;
        }
        if (enemiesSpawned > 10)
            enemiesSpawned = 1;
    }

    void SpawnEnemy(GameObject type)
    {
        Instantiate(type, generatePosition(), transform.rotation);
    }
    Vector3 generatePosition()
    {
        return new Vector3(10, Random.Range(-3.5f, 3.5f));
    }
    bool isOffScreen(Transform tr)
    {
        if (tr.position.x < -9 || Mathf.Abs(tr.position.y) > 5)
            return true;
        return false;
    }
}
