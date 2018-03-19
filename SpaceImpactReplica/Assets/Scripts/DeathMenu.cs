using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

    public Movement player;
    public GameObject deathMenu;


    // Use this for initialization

    public void OnRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (player.isDead)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
