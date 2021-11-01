using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;

    public GameObject explosion;

    public int damageAmout = 10;


    void Start()
    {
      
        Invoke("DestroyFireball", lifetime);
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyFireball()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            
            collision.GetComponent<Enemy>().TakeDamage(damageAmout);
            DestroyFireball();
        }
    }
}
