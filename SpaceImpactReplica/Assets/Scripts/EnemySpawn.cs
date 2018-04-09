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
        timer = 9;
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
        int spawnLocation = Random.Range(-4, 0);
        int spawnOption = Random.Range(1, 5);

        if (spawnOption == 1)
        {
            for (int i = 0; i < 3; i++)
                Instantiate(type, new Vector3(10, spawnLocation + i), transform.rotation);

        }

        else if (spawnOption == 2)
        {
            for (int i = 0; i < 5; i++)
                Instantiate(type, new Vector3(10-i, spawnLocation + i+0.5f), transform.rotation);
        }

        else if (spawnOption == 3)
        {
            for (int i = 0; i < 3; i++)
                Instantiate(type, new Vector3(10, spawnLocation + (1+i+0.5f)), transform.rotation);
        }

        else if (spawnOption == 4)
        {
            for (int i = 0; i < 5; i++)
                Instantiate(type, new Vector3(10 +i, spawnLocation + i+0.5f), transform.rotation);
        }

    }
    
    bool isOffScreen(Transform tr)
    {
        if (tr.position.x < -9 || Mathf.Abs(tr.position.y) > 5)
            return true;
        return false;
    }

    
}
