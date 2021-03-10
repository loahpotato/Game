using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Boss/Actions/Shoot Player", fileName = "ActionBossShootPlayer")]
public class ActionBossShootPlayer_lsy : AIAction_lsy
{
    public float shotDelay = 1f;  // Will be aggressive shooting

    private float nextShotTime;

    public override void Act(StateController_lsy controller)
    {
        nextShotTime -= Time.deltaTime;
        Shoot(controller);
        Debug.Log("acting");
    }

    private void Shoot(StateController_lsy controller)
    {
        if (nextShotTime <= 0)
        {
            if (controller.PlayerHealth.CurrentHealth >= 7)
            {
                controller.BossSpiralPattern.EnableProjectile();
            }

            if (controller.PlayerHealth.CurrentHealth >= 4 && controller.PlayerHealth.CurrentHealth < 7)
            {
                controller.BossRandomPattern.EnableProjectile();
            }

            if (controller.PlayerHealth.CurrentHealth < 4)
            {
                controller.BossCirclePattern.EnableProjectile();
            }

            nextShotTime = shotDelay;
        }
    }

    private void OnEnable()
    {
        nextShotTime = shotDelay;
    }
}
