using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {

    float timer;
    public int pointsDropped = 150;
    public float health = 150f;
    float savedHealth;
    public float firingRate = 0.5f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
    public float movementSpeed = 2f;
    public float chargeBarValue = 10;
    public GameObject Drop1;
    public GameObject TripleShot;
    public GameObject projectile;
    GameObject bulletPool;
    ObjectPooler bulletPooler;
    GameObject explosionPool;
    ObjectPooler explosionPooler;

    private void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("EnemyBulletPool");
        bulletPooler = bulletPool.GetComponent<ObjectPooler>();
        projectile = bulletPooler.pooledObject;
        explosionPool = GameObject.FindGameObjectWithTag("ExplosionPool");
        explosionPooler = explosionPool.GetComponent<ObjectPooler>();
        savedHealth = health;
    }
    private void Fire() {
        Vector3 position = transform.position + new Vector3(-0.8f, 0f);
        GameObject missle = bulletPooler.GetPooledObject(position, transform.rotation);
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
            
            if (health <= 0)
            {
                FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
                test.value += chargeBarValue;

                if (Random.Range(1f, 100f) <= 20f)
                {
                    Vector3 position = transform.position + new Vector3(0f, -0.8f);
                    Instantiate(Drop1, position, transform.rotation);
                }
                if (Random.Range(1f, 100f) > 95f)
                {
                    Vector3 position = transform.position + new Vector3(0f, -0.8f);
                    Instantiate(TripleShot, position, TripleShot.transform.rotation);
                }

                explosionPooler.GetPooledObject(transform.position, transform.rotation).transform.localScale += new Vector3(0,0);
            }

            Destroy();
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerProjectile") {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            ReceiveDamage(player.damage);            
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
    void Destroy()
    {
        gameObject.SetActive(false);
        health = savedHealth;
    }
}
