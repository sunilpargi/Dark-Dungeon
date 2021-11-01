using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveAmout;

    public int health = 40;

    public GameObject[] healthUI;
    public Text scoreText;
    private int scoreCount;

    public HurtScreen hurtScreen;

    public AudioClip hurtClip, dieClip, pickupClip;
    public AudioSource audioSource, pickupAudioSource;

    public Transform spawnPoint;

    Weapon weapon;
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

       
        scoreText.text = scoreCount.ToString();
        spawnPoint.GetComponentInChildren<Weapon>().equipped = true;
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmout = moveInput.normalized * speed;

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmout * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        hurtScreen.ActivateObj();
        health -= damage;
        healthUI[health].SetActive(false);
        audioSource.PlayOneShot(hurtClip,1);
        if (health <= 0)
        {
            audioSource.PlayOneShot(dieClip,1);
            healthUI[health].SetActive(false);
            Destroy(gameObject);
        }
    }

    public void changeWeapon(Weapon weaponToEquip)
    {
        if(weapon != null)
        {
            weapon.gameObject.SetActive(false);
         
        }
        
            //Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            weapon = Instantiate(weaponToEquip, spawnPoint.position, spawnPoint.rotation, spawnPoint) as Weapon;
        
       
        weapon.equipped = true;
    }

    public void UpdateScoreText()
    {
        scoreCount += 20;
        scoreText.text = scoreCount.ToString();
    }

    public void PickUPSound()
    {
        pickupAudioSource.PlayOneShot(pickupClip,1);
    }
}
