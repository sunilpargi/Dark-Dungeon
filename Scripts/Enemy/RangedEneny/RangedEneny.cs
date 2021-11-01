using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEneny : Enemy
{
    public float stopDistance;

    private float attackTime;

    private Animator anim;

    public Transform shotPoints;

    public GameObject enemybullets;

   public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //if (Vector2.Distance(transform.position, player.position) > stopDistance)
            //{
            //    Debug.Log(player.position);
            //    Debug.Log(transform.position);
            //    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                
            //}

            //if (Time.time > attackTime)
            //{
            //    attackTime = Time.time + timeBetweenAttack;
            //    anim.SetTrigger("attack");
            //}

            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);


            }
            else
            {
             
                if (Time.time > attackTime)
                {
                    Debug.Log(1);
                    attackTime = Time.time + timeBetweenAttack;
                    anim.SetTrigger("attack");

                }
            }
        }



    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoints.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoints.rotation = rotation;

        Instantiate(enemybullets, shotPoints.position, shotPoints.rotation);
    }
}
