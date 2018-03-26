using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public float speed;
    public float health = 200f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
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
    }

    void FixedUpdate()
    {
        SpecialAttack();
        Move();
        FireAtSpecifiedRate();
    }

    void FireAtSpecifiedRate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        timer += Time.deltaTime;
        if (timer > shotsPerSecond)
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
        float newX = Mathf.Clamp(transform.position.x, -8, 8);
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
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            health -= 70;
            healthbar.value -= 70;
        }
        else if (collision.gameObject.tag == "HealthUp")
        {
            health += 50;
            healthbar.value += 50;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
        
    }

    private void Fire()
    {
        Vector3 position = transform.position + new Vector3(0.8f, 0f);
        GameObject missile = Instantiate(projectile, position, transform.rotation/*Quaternion.identity*/) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
    }

}
