using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    public GameObject powerup1;
    public GameObject powerup2;
    public GameObject powerup3;
    float[] yPos = {2.5f,0f,-2.5f};
    float xPos = 10;
    int numberOfEnemies;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        numberOfEnemies = GetComponent<EnemySpawn>().enemiesSpawned;

        if (numberOfEnemies == 10)
        {            
            Instantiate(powerup1, new Vector3(xPos, yPos[0]), Quaternion.identity);
            Instantiate(powerup2, new Vector3(xPos, yPos[1]), Quaternion.identity);
            Instantiate(powerup3, new Vector3(xPos, yPos[2]), Quaternion.identity);
            GetComponent<EnemySpawn>().enemiesSpawned = 1;
        }
        if (powerup1.GetComponent<Powerup>().Triggerred || powerup2.GetComponent<Powerup>().Triggerred || powerup3.GetComponent<Powerup>().Triggerred)
        {
            Destroy(powerup1);
            Destroy(powerup2);
            Destroy(powerup3);
        }
    }
    public void DestroyPowerUps()
    {
        Destroy(powerup1.gameObject);
        Destroy(powerup2.gameObject);
        Destroy(powerup3.gameObject);
    }
    
    
}
