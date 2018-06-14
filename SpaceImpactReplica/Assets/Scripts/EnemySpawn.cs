﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {
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

    GameObject EnemyPool1;
    ObjectPooler EnemyPooler1;
    GameObject EnemyPool2;
    ObjectPooler EnemyPooler2;
    GameObject EnemyPool3;
    ObjectPooler EnemyPooler3;
    GameObject enemyType;
    GameObject enemyType2;
    GameObject enemyType3;
    GameObject lastSpawned;
    public GameObject enemyType4;

    GameObject EPool1;
    ObjectPooler EPooler1;
    GameObject enemyType5;
    GameObject EPool2;
    ObjectPooler EPooler2;
    GameObject enemyType6;
    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        transform.Rotate(new Vector3(0, 0, -90));
        timer = 7;
        EnemyPool1 = GameObject.FindGameObjectWithTag("EnemyPool1");
        EnemyPooler1 = EnemyPool1.GetComponent<ObjectPooler>();
        EnemyPool2 = GameObject.FindGameObjectWithTag("EnemyPool2");
        EnemyPooler2 = EnemyPool2.GetComponent<ObjectPooler>();
        EnemyPool3 = GameObject.FindGameObjectWithTag("EnemyPool3");
        EnemyPooler3 = EnemyPool3.GetComponent<ObjectPooler>();
        EPool1 = GameObject.FindGameObjectWithTag("EPool1");
        EPooler1 = EPool1.GetComponent<ObjectPooler>();
        EPool2 = GameObject.FindGameObjectWithTag("EPool2");
        EPooler2 = EPool2.GetComponent<ObjectPooler>();
        enemyType = EnemyPooler1.pooledObject;
        enemyType2 = EnemyPooler2.pooledObject;
        enemyType3 = EnemyPooler3.pooledObject;
        enemyType5 = EPooler1.pooledObject;
        enemyType6 = EPooler2.pooledObject;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= spawnTimeIntervals)
        {
            if (SpawnEnemies)
            {
                SpawnEnemy(chooseEnemyType(enemyType,enemyType2,enemyType3,enemyType4, enemyType5, enemyType6));
                timer = 0;
                enemiesSpawned++;

                if (enemiesSpawned > 5)
                {
                    enemiesSpawned = 0;
                    SpawnEnemies = false;
                    SpawnBoss = true;
                }
            }
            if (SpawnBoss && timer >= spawnTimeIntervals)
            {
                SpawnBoss = false;
                Instantiate(boss, new Vector3(12f, 0f), boss.transform.rotation);
                healthSlider.gameObject.SetActive(true);
                effectSlider.gameObject.SetActive(true);
            }
        }
    }
    GameObject chooseEnemyType(GameObject type1, GameObject type2, GameObject type3, GameObject type4, GameObject type5, GameObject type6)
    {
        int var = Random.Range(0, 7);
        switch (var)
        {
            case 1:
                return type1;
            case 2:
                return type2;
            case 3:
                return type3;
            case 4:
                return type4;
            case 5:
                return type6;
            default:
                return type5;
        }
    }
    void SpawnEnemy(GameObject type)
    {
        while (type == lastSpawned)
        {
            type = chooseEnemyType(enemyType, enemyType2, enemyType3, enemyType4, enemyType5, enemyType6);
        }
        lastSpawned = type;
        GameObject typ = type;
        if(typ == enemyType5)
        {
            EPooler1.GetPooledObject(new Vector3(10, 4), transform.rotation);
            typ = enemyType;
        }
        if (typ == enemyType4)
        {
            Instantiate(type, new Vector3(10, 4), typ.transform.rotation);
            SpawnEnemies = false;
            return;
        }
        if (typ == enemyType3)
        {
            EnemyPooler3.GetPooledObject(new Vector3(10, 4), transform.rotation);
            typ = enemyType;
        }
        int spawnLocation = Random.Range(-4, 0);
        int spawnOption = Random.Range(1, 4);
        for (int i = 0; i < 2; i++)
            EPooler2.GetPooledObject(new Vector3(10 + i, spawnLocation + 1 + i + 0.5f), transform.rotation);
        for (int i = 0; i < 3; i++)
            EPooler1.GetPooledObject(new Vector3(10+i, spawnLocation + 1 + i + 0.5f), transform.rotation);
        if (spawnOption == 1)
        {
            for (int i = 0; i < 5; i++)
                EnemyPooler1.GetPooledObject(new Vector3(12.5f-i, spawnLocation + i+0.5f), transform.rotation);
        }

        else if (spawnOption == 2)
        {
            for (int i = 0; i < 3; i++)
                EnemyPooler2.GetPooledObject(new Vector3(10, spawnLocation + (1+i+0.5f)), transform.rotation);
        }

        else if (spawnOption == 3)
        {
            for (int i = 0; i < 3; i++)
                EPooler1.GetPooledObject(new Vector3(10 + i, spawnLocation + 1 + i + 0.5f), transform.rotation);
        }
        else if (spawnOption == 4)
        {
            for (int i = 0; i < 3; i++)
                EPooler2.GetPooledObject(new Vector3(10 + i, spawnLocation + 1 + i + 0.5f), transform.rotation);
        }

    }
    
    bool isOffScreen(Transform tr)
    {
        if (tr.position.x < -9 || Mathf.Abs(tr.position.y) > 5)
            return true;
        return false;
    }

    
}
