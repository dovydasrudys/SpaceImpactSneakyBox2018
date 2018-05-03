using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Movement player;
    public GameObject deathMenu;
    public GameObject GameControl;
    public Text lvlComplete;
    public int HowManyBosses;
    string bbd;


    // Use this for initialization

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SpaceImpactReplica");

    }

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (player.isDead)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0;
            lvlComplete.text = "YOU DIED!"+"\n"+"Your score: " + player.maxPoints;
            GameObject.FindGameObjectWithTag("ProceedButton").SetActive(false);
            GameObject.FindGameObjectWithTag("PlayButton").SetActive(true);
        }
        if(GameControl.GetComponent<EnemySpawn>().bossesBeaten == HowManyBosses)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0;
            lvlComplete.text = "LEVEL COMPLETED! \nYour score: " +  player.maxPoints;
            GameObject.FindGameObjectWithTag("PlayButton").SetActive(false);
            GameObject.FindGameObjectWithTag("ProceedButton").SetActive(true);
        }
    }
    public void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
