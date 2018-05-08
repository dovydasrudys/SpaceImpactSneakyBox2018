using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage = 100f;

    private void Update()
    {
        if (isOffScreen())
            Destroy();
    }
    public float GetDamage() {
        return damage;
    }

    public void Hit() {
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
    }
    
}
