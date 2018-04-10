using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    public float Health;
    public float FiringRate;
    public float Damage;
    public bool Triggerred = false;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * 1f);
        if (isOffScreen())
            Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")        
    //        Hit();
    //    Debug.Log("1");
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Triggerred = true;
        if (collision.gameObject.tag == "Player")
            Hit();
        Destroy(GameObject.FindGameObjectWithTag("Prize"));
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    bool isOffScreen()
    {
        if (transform.position.x < -10 )
            return true;
        return false;
    }
}
