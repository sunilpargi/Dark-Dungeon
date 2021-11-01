using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public int health = 20;
    public float speed;

    [HideInInspector]
    public Transform player;

    public float timeBetweenAttack;
    public int damage = 10;

    public int pickUpChance;
    public GameObject[] pickUps;
    public AudioClip hurtClip, dieClip, bloodSlash;
    public AudioSource audioSource, gameoverAudiosource;
    public GameObject deathParticle;
    public GameObject[] deathStain;

    public virtual void Start()
    {
        audioSource = GameObject.Find("GameSound").GetComponent<AudioSource>();
        gameoverAudiosource = GameObject.Find("GameoverSound").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damage)
    {
      
        health -= damage;
        audioSource.PlayOneShot(hurtClip, 1);

        if (health < 0)
        {
            audioSource.PlayOneShot(dieClip, 1);
            gameoverAudiosource.PlayOneShot(bloodSlash, 1);
            player.gameObject.GetComponent<Player>().UpdateScoreText();
            int randomNumer = Random.Range(0, 30);
            if(randomNumer < pickUpChance)
            {
                GameObject randomPickUp = pickUps[Random.Range(0, pickUps.Length)];
                Instantiate(randomPickUp, transform.position, transform.rotation);
              
            }
            Instantiate(deathParticle, transform.position, transform.rotation);
            Instantiate(deathStain[Random.Range(0, deathStain.Length)], transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

  
}
