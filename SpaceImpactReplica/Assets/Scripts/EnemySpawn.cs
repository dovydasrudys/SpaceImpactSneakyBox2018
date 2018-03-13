using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemyType;
    public int maxNumberOfEnemies;
    public int spawnTimeIntervals;
    int enemyNumber = 0;
    float timer = 0f;
    List<GameObject> enemies = new List<GameObject>();
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
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if(isOffScreen(enemies[i].transform))
            {
                Destroy(enemies[i]);
                enemies.RemoveAt(i);
            }
        }
	}

    void SpawnEnemy(GameObject type)
    {
        if (enemies.Count >= maxNumberOfEnemies)
            return;
        else
        {
            enemies.Add(Instantiate(type, generatePosition(), transform.rotation));
        }
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
