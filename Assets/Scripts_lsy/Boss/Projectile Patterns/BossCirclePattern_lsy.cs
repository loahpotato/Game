using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCirclePattern_lsy : BossBaseShot_lsy
{
    //protected override void Start()  //REMOVE this Start method
    //{
    //base.Start();
    //EnableShooting();
    //}

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (isShooting == false)
        {
            return;
        }

        float shiftAngle = 360f / projectileAmount;
        for (int i = 0; i < projectileAmount; i++)
        {
            BossProjectile_lsy bossProjectile = GetBossProjectile(transform.position);
            if (bossProjectile == null)
            {
                break;
            }

            float angle = shiftAngle * i;
            ShootProjectile(bossProjectile, projectileSpeed, angle, projectileAcceleration);
        }

        DisableShooting();
    }

    public void EnableProjectile()
    {
        isShooting = true;
    }
}
