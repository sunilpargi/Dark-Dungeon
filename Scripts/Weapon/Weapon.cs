using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject fireBall;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    public AudioClip shootClip;
    public AudioSource audioSource;

   public bool equipped;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("GameSound").GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!equipped) return;

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //transform.rotation = rotation;


        if (Input.GetMouseButton(0))
        {
            audioSource.PlayOneShot(shootClip, 0.5f);
            if (Time.time > shotTime)
            {
                Instantiate(fireBall, shotPoint.position, shotPoint.rotation);

                shotTime = Time.time + timeBetweenShots;
            }
        }
    }

}
