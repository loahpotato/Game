using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth_lsy : Collectables_lsy
{
    [SerializeField] private int healthToAdd = 1;
    [SerializeField] private ParticleSystem healthBonus;

    protected override void Pick()
    {
        AddHealth();
    }

    protected override void PlayEffects()
    {
        Instantiate(healthBonus, transform.position, Quaternion.identity);
    }

    public void AddHealth()
    {
        if (character != null)
        {
            character.GetComponent<Health_lsy>().GainHealth(healthToAdd);
        }
    }
}

