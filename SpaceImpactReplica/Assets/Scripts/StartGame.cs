using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    // Use this for initialization
    public GameObject startMenuUI;



    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void OnStart()
    {
        Time.timeScale = 1f;
        startMenuUI.SetActive(false);

    }

    public void onQuit()
    {
        Application.Quit();
    }
}
