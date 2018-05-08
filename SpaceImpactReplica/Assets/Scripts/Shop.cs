using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    public GameObject powerup1;
    public GameObject powerup2;
    public GameObject powerup3;
    int numberOfEnemies;
	// Use this for initialization
	void Start () {
        Instantiate(powerup1, new Vector3(8, 2.5f), powerup1.transform.rotation);
        Instantiate(powerup2, new Vector3(8, 0), powerup1.transform.rotation);
        Instantiate(powerup3, new Vector3(8, -2.5f), powerup1.transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (powerup1.GetComponent<Powerup>().Triggerred || powerup2.GetComponent<Powerup>().Triggerred || powerup3.GetComponent<Powerup>().Triggerred)
        {
            DestroyImmediate(powerup1,true);
            DestroyImmediate(powerup2,true);
            DestroyImmediate(powerup3,true);
        }
    }
    public void DestroyPowerUps()
    {
        DestroyImmediate(powerup1.gameObject);
        DestroyImmediate(powerup2.gameObject);
        DestroyImmediate(powerup3.gameObject);
    }
    
    
}
