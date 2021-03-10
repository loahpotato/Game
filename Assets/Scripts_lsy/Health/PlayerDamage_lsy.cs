using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage_lsy : MonoBehaviour
{
    [SerializeField] private Character_lsy.CharacterTypes damageType = Character_lsy.CharacterTypes.AI;
    [SerializeField] private int damageToApply = 1;

    private Health_lsy playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<Health_lsy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && other.gameObject.layer != 13) //13 is PlayerProjectile layer
        {
            playerHealth.TakeDamage(damageToApply);
        }
    }
}
