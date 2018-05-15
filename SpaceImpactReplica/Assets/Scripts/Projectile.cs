using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;
    public float damage = 100f;

    private void Update()
    {
        if (isOffScreen())
            Destroy();
    }
    public float GetDamage()
    {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "PlayerProjectile" && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "SpecialMove" || collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "HealthUp" || collision.gameObject.tag == "TripleShot"))
            return;
        if (gameObject.tag == "EnemyProjectile" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "SpecialMove" || collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "HealthUp" || collision.gameObject.tag == "TripleShot"))
            return;
        Instantiate(explosion, collision.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
    }

    public void Hit()
    {
        Destroy();
    }
    bool isOffScreen()
    {
        if (transform.position.x < -9 || transform.position.x > 15)
            return true;
        return false;
    }
    void Destroy()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(0, 0, 0);
    }

}
