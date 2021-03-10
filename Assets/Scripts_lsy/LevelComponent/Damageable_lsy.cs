using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable_lsy : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    //Weapon recoil;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health_lsy>().TakeDamage(damage);

        }
    }

}
    
