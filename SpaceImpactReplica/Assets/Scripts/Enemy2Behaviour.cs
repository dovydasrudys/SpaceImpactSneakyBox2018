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


    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; //target the player
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        int yDir;
        if (player.position.y - transform.position.y < -1)
            yDir = 1;
        else if (player.position.y - transform.position.y > 1)
            yDir = -1;
        else
            yDir = 0;
        transform.Translate(new Vector2(yDir,-1) * Time.deltaTime * movementSpeed);

        if (isOffScreen() || health <= 0) {
            Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();

            if (health <= 0)
                test.value += chargeBarValue;
            Destroy(gameObject);

        }
        //transform.Translate(new Vector2(transform.position.x, player.transform.position.y) * Time.deltaTime * movementSpeed);
        /*Vector3 forwardAxis = new Vector3(0, 0, -1);




        transform.LookAt(player.position, forwardAxis);
        Debug.DrawLine(transform.position, player.position);
        transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
        transform.position -= transform.TransformDirection(Vector2.up) * movementSpeed;


        //if (/*isOffScreen() || health <= 0)
        //{ 
            
        //    if (health <= 0)
        //    {
        //        Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();
        //        test.value += chargeBarValue;
        //    }
        //    Destroy(gameObject);
        //}*/


    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            ReceiveDamage(missile.GetDamage());
            FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
            missile.Hit();
        }
        else if (collision.gameObject.tag == "Player")
        {
            ReceiveDamage(health);
        }
        else if (collision.gameObject.tag == "SpecialMove")
        {
            ReceiveDamage(health);
            FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
        }
        if (health <= 0)
            Destroy(gameObject);

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
