using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemyType;
    public GameObject boss;
    public GameObject prize;
    public int spawnTimeIntervals;
    public int enemiesSpawned;
    float timer = 0f;
    public bool SpawnEnemies = true;
    public bool SpawnBoss = false;

    Slider healthSlider;
    Slider effectSlider;


    // Use this for initialization
    void Start () {
        effectSlider = GameObject.FindGameObjectWithTag("Effect").GetComponent<Slider>();
        healthSlider = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Slider>();
        healthSlider.gameObject.SetActive(false);
        effectSlider.gameObject.SetActive(false);
        transform.Rotate(new Vector3(0, 0, -90));
        timer = 9;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= spawnTimeIntervals)
        {
            if (SpawnEnemies)
            {
                //if (timer >= spawnTimeIntervals)
                //{
                    SpawnEnemy(enemyType);
                    timer = 0;
                    enemiesSpawned++;
                //}
                if (enemiesSpawned == 5)
                {
                    enemiesSpawned = 0;
                    SpawnEnemies = false;
                    SpawnBoss = true;
                }
            }
            if (SpawnBoss && timer >= spawnTimeIntervals)
            {
                SpawnBoss = false;
                Instantiate(boss, new Vector3(5.5f, 0f), boss.transform.rotation);
                healthSlider.gameObject.SetActive(true);
                effectSlider.gameObject.SetActive(true);
            }
        }
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
