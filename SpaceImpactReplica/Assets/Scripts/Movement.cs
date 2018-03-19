using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float health = 200f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
    float timer;
    public GameObject projectile;

    Vector3 movement = new Vector3();
    float h;
    float v;
    public bool isDead = false;

    void FixedUpdate()
    {
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
            missile.Hit();
            if (health <= 0) {
                Destroy(gameObject);
                isDead = true;
            }
        }
    }

    private void Fire()
    {
        Vector3 position = transform.position + new Vector3(0.8f, 0f);
        GameObject missile = Instantiate(projectile, position, transform.rotation/*Quaternion.identity*/) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
    }

}
