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
        if (gameObject.tag == "PlayerProjectile")
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                case "Enemy2":
                case "Enemy3":
                case "Boss":
                    Instantiate(explosion, collision.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                default:
                    break;
            }
        }
        else if (gameObject.tag == "EnemyProjectile")
        {
            switch (collision.gameObject.tag)
            {
                case "Player":
                    Instantiate(explosion, collision.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                default:
                    break;
            }
        }
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
