﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Behaviour : MonoBehaviour
{

    public float hitPoints;

    public float projectileSpeed = 5f;
    public GameObject prize;
    public GameObject slide;
    public GameObject projectile;
    public GameObject position1;
    public GameObject position2;
    public float shotsPerSecond = 2f;
    public float restTime = 8f;


    float timer = 0;
    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;
    Slider healthSlider;
    Slider effectSlider;
    //bool performeAttack = true;
    bool dealDamage = false;
    bool doAction = true;
    float specialAttackDmg = 200f;


    // Use this for initialization
    void Start()
    {

        effectSlider = GameObject.FindGameObjectWithTag("Effect").GetComponent<Slider>();
        healthSlider = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Slider>();
        healthSlider.maxValue = hitPoints;
        healthSlider.value = hitPoints;
        effectSlider.maxValue = healthSlider.value;
        effectSlider.value = healthSlider.value;
    }
    void TakeDamage()
    {
        float bound = 0.5f;
        timer2 += Time.deltaTime;
        if (timer2 >= bound)
        {
            timer2 = 0;
            healthSlider.value -= specialAttackDmg;
            hitPoints -= specialAttackDmg;
        }
    }

    private void FirstAttack()
    {
        if (doAction)
        {
            timer += Time.deltaTime;
            if (timer > shotsPerSecond)
            {
                timer = 0;
                Vector3 _position1 = position1.transform.position;
                GameObject missle = Instantiate(projectile, _position1, transform.rotation/*Quaternion.identity*/) as GameObject;
                Vector3 _position2 = position2.transform.position;
                Instantiate(projectile, _position2, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            healthSlider.value -= missile.GetDamage();
            hitPoints -= missile.GetDamage();
            missile.Hit();
        }
        if (collision.gameObject.tag == "SpecialMove")
        {
            healthSlider.value -= specialAttackDmg;
            hitPoints -= specialAttackDmg;
            dealDamage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpecialMove")
        {
            dealDamage = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 5.5)
        {
            transform.Translate(new Vector2(0, 0.03f));
            return;
        }
        timer1 += Time.deltaTime;
        if (timer1 > restTime)
        {
            if (doAction)
            {
                doAction = false;
                restTime = 5f;
                timer1 = 0;
            }
            else
            {
                doAction = true;
                restTime = 8f;
                timer1 = 0;
            }
        }
        FirstAttack();

        if (effectSlider.value > healthSlider.value)
        {
            timer3 += Time.deltaTime;
            if (timer3 > 1.5)
            {
                effectSlider.value = healthSlider.value;
                timer3 = 0f;
            }
        }

        if (dealDamage)
        {
            TakeDamage();
        }

        if (hitPoints <= 0)
        {

            GameObject.FindGameObjectWithTag("BossHealth").SetActive(false);
            GameObject.FindGameObjectWithTag("Effect").SetActive(false);
            GameObject.FindGameObjectWithTag("GameControl").GetComponent<EnemySpawn>().SpawnEnemies = true;
            GameObject.FindGameObjectWithTag("GameControl").GetComponent<EnemySpawn>().bossesBeaten++;
            Instantiate(prize, new Vector3(8, 0), prize.transform.rotation);
            Destroy(gameObject);
        }
    }
}
