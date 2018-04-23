using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public float speed;
    public float health = 200f;
    public float maxHealth = 200f;
    public float secondsPerShot = 2f;
    public float projectileSpeed = 5f;
    public int points;
    float timer;
    Slider special;
    Slider healthbar;
    public GameObject projectile;
    public GameObject ulti;

    Vector3 movement = new Vector3();
    float h;
    float v;
    public bool isDead = false;

    private void Start() {
        special = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();
        healthbar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        projectile.GetComponent<Projectile>().damage = 200f;
    }

    void FixedUpdate()
    {
        SpecialAttack();
        Move();
        FireAtSpecifiedRate();
        //Debug.Log(projectile.GetComponent<Projectile>().damage);
    }

    void FireAtSpecifiedRate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        timer += Time.deltaTime;
        if (timer > secondsPerShot)
        {
            Fire();
            timer = 0;
        }
    }

    void SpecialAttack() {

        if(Input.GetKeyDown("space") && special.value == 100) {
            ulti.SetActive(true);
            special.value = 0;
        }
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        movement.Set(h, v, 0);
        transform.localPosition += movement * speed;
        float newX = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float newY = Mathf.Clamp(transform.position.y, -4, 4);
        transform.localPosition = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            health -= missile.GetDamage();
            healthbar.value -= missile.GetDamage();
            missile.Hit();
        } else if (collision.gameObject.tag == "Boss") {
            health -= 300;
            healthbar.value -= 300;  
        } else if (collision.gameObject.tag == "Enemy")
        {
            health -= 70;
            healthbar.value -= 70;
            IncreasePoints(collision.gameObject.GetComponent<EnemyBehaviour>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            Enemy2Behaviour enemy = collision.gameObject.GetComponent<Enemy2Behaviour>();
            health -= enemy.damage;
            healthbar.value -= enemy.damage;
            IncreasePoints(collision.gameObject.GetComponent<Enemy2Behaviour>().pointsDropped);
        }
        else if (collision.gameObject.tag == "HealthUp")
        {
            health += 50;
            healthbar.value += 50;
            if (health > maxHealth)
                health = maxHealth;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "PowerUp")
        {
            if (points >= 1000)
            {
                GameObject pwup = collision.gameObject;
                healthbar.maxValue *= pwup.GetComponent<Powerup>().Health;
                maxHealth *= pwup.GetComponent<Powerup>().Health;
                secondsPerShot *= pwup.GetComponent<Powerup>().FiringRate;
                projectile.GetComponent<Projectile>().damage *= pwup.GetComponent<Powerup>().Damage;

                //FindObjectOfType<Shop>().DestroyPowerUps();
                //pwup.GetComponent<Powerup>().Hit();
                IncreasePoints(-1000);
                pwup.GetComponent<Powerup>().Hit();
            }
            
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
        
    }

    public void IncreasePoints(int pointsAdded)
    {
        points += pointsAdded;
    }
    private void Fire()
    {
        Vector3 position = transform.position + new Vector3(0.8f, 0f);
        GameObject missile = Instantiate(projectile, position, transform.rotation/*Quaternion.identity*/) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
    }

}
