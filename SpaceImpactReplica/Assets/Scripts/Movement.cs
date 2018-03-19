using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float health = 200f;

    private Vector2 direction = Vector2.zero;
    Vector3 movement = new Vector3();
    float h;
    float v;
    public bool isDead = false;

    void FixedUpdate()
    {
        Move();
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
        Projectile missle = collision.gameObject.GetComponent<Projectile>();
        if (missle && collision.gameObject.tag == "EnemyProjectile")
        {
            health -= missle.GetDamage();
            missle.Hit();
            if (health <= 0) {
                Destroy(gameObject);
                isDead = true;
            }
        }
    }




}
