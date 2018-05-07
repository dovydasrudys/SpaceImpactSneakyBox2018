using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour3 : MonoBehaviour {

    float timer;
    public int pointsDropped = 150;
    public float health = 150f;
    public float firingRate = 0.5f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
    public float movementSpeed = 2f;
    public float chargeBarValue = 10;
    public GameObject bomb;

    private void Fire() {
        Vector3 position = transform.position + new Vector3(1f, 0f);
        //Instantiate(bomb, position, transform.rotation/*Quaternion.identity*/);
        GameObject rocket = Instantiate(bomb);
        rocket.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
    }

    private void Update() {
        transform.Translate(Vector2.down * Time.deltaTime * movementSpeed);
        timer += Time.deltaTime;
        if (timer > shotsPerSecond) {
            Fire();
            timer = 0;
        }
        if (isOffScreen() || health <= 0) {
            Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();            
            
            if (health <= 0)
            {
                FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
                test.value += chargeBarValue;
            }
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerProjectile") {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            ReceiveDamage(missile.GetDamage());            
            missile.Hit();
        }
        else if (collision.gameObject.tag == "Player")
            ReceiveDamage(health);

        else if (collision.gameObject.tag == "SpecialMove") 
            ReceiveDamage(health);
        
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        
    }
    bool isOffScreen()
    {
        if (transform.position.x < -9 || Mathf.Abs(transform.position.y) > 5)
            return true;
        return false;
    }
}
