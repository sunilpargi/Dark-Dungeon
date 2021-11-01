using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleEnemy : Enemy
{
    public float stopDistance;

    public float attackTime;
    public float attackSpeed;


    public virtual void Start()
    {
        base.Start();
    }

    void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time > attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
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
        while(percent <= 1)
        {
            percent += Time.time * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula * Time.deltaTime);
            yield return null;
        }

    }
}
