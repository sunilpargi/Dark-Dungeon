﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summnor : Enemy
{
    public float minX;
    public float minY;
    public float maxY;
    public float maxX;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummon;
    private float summonTime;

    public GameObject enemyToSummon;

    public float attackSpeed;
    public float stopDistance;
    public float attackTime;


   public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        if(player != null)
        {

            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
             
            }
            else
            {
                anim.SetBool("isRunning", false);

                if(Time.time > summonTime)
                {
                    summonTime = Time.time + timeBetweenSummon;
                    anim.SetTrigger("Summon");

                }
            }

            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                if (Time.time > attackTime)
                {
                    
                    attackTime = Time.time + timeBetweenAttack;
                    StartCoroutine(Attack());
                }
            }
        }
        
    }
    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while (percent <= 1)
        {
            percent += Time.time * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula * Time.deltaTime);
            yield return null;
        }

    }



    public void Summon()
    {
        Instantiate(enemyToSummon, transform.position, transform.rotation);
 
    }
}
 