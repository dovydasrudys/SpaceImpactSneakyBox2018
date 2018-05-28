using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    bool x3Activated = false;
    float x3Timer;
    GameObject bulletPool;
    ObjectPooler bulletPooler;


    public float speed;
    public float health;
    float maxHealth;
    public float secondsPerShot;
    public float projectileSpeed;
    public int points;
    public int maxPoints = 0;
    public float damage;
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
        projectile.GetComponent<Projectile>().damage = damage;
        bulletPool = GameObject.FindGameObjectWithTag("PlayerBulletPool");
        bulletPooler = bulletPool.GetComponent<ObjectPooler>();
        projectile = bulletPooler.pooledObject;
        maxHealth = health;
    }


    void FixedUpdate()
    {
        SpecialAttack();
        Move();
        FireAtSpecifiedRate();
    }

    void FireAtSpecifiedRate()
    {
        timer += Time.deltaTime;
        x3Timer += Time.deltaTime;
        if (timer > secondsPerShot)
        {
            Fire();
            timer = 0;
        }
        if (x3Activated)
        {
            if (x3Timer > 10)
                x3Activated = false;
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
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Enemy2Behaviour>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            collision.gameObject.GetComponent<EnemyBehaviour3>();
            health = 0;
            healthbar.value = 0;
        }
        else if (collision.gameObject.tag == "Enemy4")
        {
            EnemyBehaviour4 enemy = collision.gameObject.GetComponent<EnemyBehaviour4>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<EnemyBehaviour4>().pointsDropped);
        }
        else if (collision.gameObject.tag == "HealthUp")
        {
            health += 50;
            healthbar.value += 50;
            if (health > maxHealth)
                health = maxHealth;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "TripleShot")
        {
            x3Activated = true;
            x3Timer = 0;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bomb")
        {
            health = 0;
            healthbar.value = 0;
            collision.gameObject.GetComponent<Bomb>().Destroy();
        }
        else if (collision.gameObject.tag == "PowerUp")
        {
            if (points >= 1000)
            {
                GameObject pwup = collision.gameObject;
                healthbar.maxValue *= pwup.GetComponent<Powerup>().Health;
                maxHealth *= pwup.GetComponent<Powerup>().Health;
                secondsPerShot *= pwup.GetComponent<Powerup>().FiringRate;
                damage *= pwup.GetComponent<Powerup>().Damage;

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
        if(pointsAdded > 0)
            maxPoints += pointsAdded;
    }
    private void Fire()
    {
        if(x3Activated)
        {
            FireThree();
            return;
        }
        Vector3 position = transform.position + new Vector3(0.8f, 0f);
        GameObject missile = bulletPooler.GetPooledObject(position, transform.rotation);
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile.GetComponent<Projectile>().damage = damage;
        //source.PlayOneShot(laserSound, 100);
    }
    private void FireThree()
    {
        Vector3 position1 = transform.position + new Vector3(0.5f, 0.5f);
        Vector3 position2 = transform.position + new Vector3(0.5f, -0.5f);
        Vector3 position3 = transform.position + new Vector3(0.8f, 0f);
        GameObject missile1 = bulletPooler.GetPooledObject(position1, transform.rotation);
        missile1.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile1.GetComponent<Projectile>().damage = damage;
        GameObject missile2 = bulletPooler.GetPooledObject(position2, transform.rotation);
        missile2.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile2.GetComponent<Projectile>().damage = damage;
        GameObject missile3 = bulletPooler.GetPooledObject(position3, transform.rotation);
        missile3.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile3.GetComponent<Projectile>().damage = damage;
    }

}
