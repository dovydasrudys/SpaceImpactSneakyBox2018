using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemyType;
    public GameObject enemyType2;
    public GameObject enemyType3;
    public GameObject boss;
    public GameObject prize;
    public int spawnTimeIntervals;
    public int enemiesSpawned;
    float timer = 0f;
    public bool SpawnEnemies = true;
    public bool SpawnBoss = false;
    public int bossesBeaten = 0;

    public Slider healthSlider;
    public Slider effectSlider;


    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
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
                    SpawnEnemy(chooseEnemyType(enemyType,enemyType2,enemyType3));
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
    GameObject chooseEnemyType(GameObject type1, GameObject type2, GameObject type3)
    {
        int var = Random.Range(1, 4);
        switch (var)
        {
            case 1:
                return type1;
            case 2:
                return type2;
            default:
                return type3;
        }
    }
    void SpawnEnemy(GameObject type)
    {
        GameObject typ = type;
        if(typ == enemyType3)
        {
            Instantiate(type, new Vector3(10, 4), transform.rotation);
            typ = enemyType;
        }
        int spawnLocation = Random.Range(-4, 0);
        int spawnOption = Random.Range(1, 3);

        if (spawnOption == 1)
        {
            for (int i = 0; i < 5; i++)
                Instantiate(typ, new Vector3(12.5f-i, spawnLocation + i+0.5f), transform.rotation);
        }

        else if (spawnOption == 2)
        {
            for (int i = 0; i < 3; i++)
                Instantiate(typ, new Vector3(10, spawnLocation + (1+i+0.5f)), transform.rotation);
        }

        else if (spawnOption == 3)
        {
            for (int i = 0; i < 5; i++)
                Instantiate(typ, new Vector3(10 +i, spawnLocation + i+0.5f), transform.rotation);
        }        

    }
    
    bool isOffScreen(Transform tr)
    {
        if (tr.position.x < -9 || Mathf.Abs(tr.position.y) > 5)
            return true;
        return false;
    }

    
}
