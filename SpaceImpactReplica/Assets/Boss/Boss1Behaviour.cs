using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Behaviour : MonoBehaviour {

    public float hitPoints;

    public float projectileSpeed = 5f;
    public GameObject projectile;
    public GameObject position1;
    public GameObject position2;
    public float shotsPerSecond = 2f;
    public float restTime = 10f;

    private bool moveUp = false;

    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;
    Slider health;
    bool performeAttack = true;
    bool dealDamage = false;
    bool doAction = true;
    float timer=0;
    float specialAttackDmg = 400f;


    // Use this for initialization
    void Start() {
        health = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Slider>();
        health.maxValue = hitPoints;
        health.value = hitPoints;
    }
    void TakeDamage() {        
        float bound = 0.5f;
        timer += Time.deltaTime;
        if (timer >= bound) {
            timer = 0;
            health.value -= specialAttackDmg;
            hitPoints -= specialAttackDmg;
        }
    }

    private void FirstAttack() {
        if (doAction) { 
            timer += Time.deltaTime;
            if (timer > shotsPerSecond) {
                timer = 0;
                Vector3 _position1 = position1.transform.position;
                GameObject missle = Instantiate(projectile, _position1, transform.rotation/*Quaternion.identity*/) as GameObject;
                Vector3 _position2 = position2.transform.position;
                Instantiate(projectile, _position2, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerProjectile") {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            health.value -= missile.GetDamage();
            hitPoints -= missile.GetDamage();
            missile.Hit();
        } if (collision.gameObject.tag == "SpecialMove") {
            health.value -= specialAttackDmg;
            hitPoints -= specialAttackDmg;
            dealDamage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "SpecialMove") {
            dealDamage = false;
        }
    }

    // Update is called once per frame
    void Update() {

        timer1 += Time.deltaTime;
        if(timer1 > restTime) {
            if (doAction) {
                doAction = false;
                restTime = 5f;
                timer1 = 0;
            } else {
                doAction = true;
                restTime = 10f;
                timer1 = 0;
            }
        }
        FirstAttack();

        if (dealDamage) {
            TakeDamage();
        }
    }
}
