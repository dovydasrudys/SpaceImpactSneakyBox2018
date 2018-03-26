using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {

    float timer;
    public float health = 150f;
    public float firingRate = 0.5f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
    public float movementSpeed = 2f;
    public float chargeBarValue = 10;
    public GameObject Drop1;
    public GameObject projectile;
    

    private void Fire() {
        Vector3 position = transform.position + new Vector3(-0.8f, 0f);
        GameObject missle = Instantiate(projectile, position, transform.rotation/*Quaternion.identity*/) as GameObject;
        missle.GetComponent<Rigidbody2D>().velocity = new Vector3(-projectileSpeed, 0f);
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
            test.value += chargeBarValue;
            if (health <= 0)
            {
                Debug.Log("healthdrop");
                Vector3 position = transform.position + new Vector3(0f, -0.8f);
                Instantiate(Drop1, position, transform.rotation);
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
        {
            ReceiveDamage(health);
        }
        else if (collision.gameObject.tag == "SpecialMove") {
            ReceiveDamage(health);
        }

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
