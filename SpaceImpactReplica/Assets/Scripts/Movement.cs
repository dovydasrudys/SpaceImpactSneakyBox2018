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

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        movement.Set(h, v, 0);

        transform.localPosition += movement * speed;
        Debug.Log(h + " " + v);
    }

    

    private void OnTriggerEnter2D(Collider2D collision) {
        Projectile missle = collision.gameObject.GetComponent<Projectile>();
        if (missle && collision.gameObject.tag == "EnemyProjectile") {
            health -= missle.GetDamage();
            missle.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }


}
