using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSetting : MonoBehaviour {

    public GameObject startMenuUI;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	public void OnStart()
    {
        Time.timeScale = 1f;
        startMenuUI.SetActive(true);
    }

    public void onQuit()
    {
        Application.Quit();
    }
}
