using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage = 100f;

    private void Update()
    {
        if (isOffScreen())
            Destroy(gameObject);
    }
    public float GetDamage() {
        return damage;
    }

    public void Hit() {
        Destroy(gameObject);
    }
    bool isOffScreen()
    {
        if (Mathf.Abs(transform.position.x) > 9 || Mathf.Abs(transform.position.y) > 5)
            return true;
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit();
    }
}
