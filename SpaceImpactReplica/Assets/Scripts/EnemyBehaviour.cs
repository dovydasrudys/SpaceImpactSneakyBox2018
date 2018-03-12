using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    float timer;
    public float health = 150f;
    public float firingRate = 0.5f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
    public float movementSpeed = 2f;
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
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Projectile missle = collision.gameObject.GetComponent<Projectile>();
        if (missle && collision.gameObject.tag == "Playerpro") {
            health -= missle.GetDamage();
            missle.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
