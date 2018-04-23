﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void LoadScene(string scene) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void ExitGame() {
        Application.Quit();
    }
}