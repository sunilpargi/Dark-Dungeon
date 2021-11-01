using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Weapon weaponToEquip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision. tag == "Player")
        {
            
            collision.GetComponent<Player>().PickUPSound();
            collision.GetComponent<Player>().changeWeapon(weaponToEquip);
            Destroy(this.gameObject);
        }
    }
}
