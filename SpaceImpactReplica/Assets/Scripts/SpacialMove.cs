using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialMove : MonoBehaviour {

    float timer = 0f;
    float lastTime = 10f;
    EnemyBehaviour enemy;

    private void Start() {
        //EnemyBehaviour enemy;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer > lastTime) {
            gameObject.SetActive(false);
            timer = 0;
        }
    }

}
