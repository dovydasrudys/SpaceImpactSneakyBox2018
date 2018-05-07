using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Behaviour : MonoBehaviour {

    
    public int pointsDropped = 150;
    public float health = 300f;
    public float movementSpeed = 2f;
    public float chargeBarValue = 10;
    public float damage = 200f;
    public Transform player;
    public float wdistance = 10f;
    public float smoothTime = 10.0f;
    private Vector3 smoothVelocity = Vector3.zero;
    float yDifferenceGoal;
    public GameObject explosion;
    public GameObject Drop1;
    public GameObject TripleShot;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; //target the player
        yDifferenceGoal = 0 - transform.position.y;
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        int yDir;
        if (player.position.y - transform.position.y < yDifferenceGoal-1)
            yDir = 1;
        else if (player.position.y - transform.position.y > yDifferenceGoal+1)
            yDir = -1;
        else
            yDir = 0;
        transform.Translate(new Vector2(yDir,-1) * Time.deltaTime * movementSpeed);
        float newY = Mathf.Clamp(transform.position.y, -4.3f, 4.3f);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.position.z);

        if (isOffScreen()) {
            
            Destroy(gameObject);

        }        
    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            ReceiveDamage(missile.GetDamage());            
            missile.Hit();
        }
        else if (collision.gameObject.tag == "Player")
        {
            ReceiveDamage(health);
        }
        else if (collision.gameObject.tag == "SpecialMove")
        {
            ReceiveDamage(health);
        }
        if (health <= 0)
        {
            FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
            Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();
            test.value += chargeBarValue;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
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
        }

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
}
