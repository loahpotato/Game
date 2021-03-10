using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseShot_lsy : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] protected int projectileAmount = 35;
    [SerializeField] protected float projectileSpeed = 2f;
    [SerializeField] protected float projectileAcceleration = 0f;

    protected ObjectPooler_lsy pooler;
    protected bool isShooting;

    protected virtual void Start()
    {
        pooler = GetComponent<ObjectPooler_lsy>();
    }

    protected BossProjectile_lsy GetBossProjectile(Vector3 newPosition)
    {
        GameObject bossProjectilePooled = pooler.GetObjectFromPool();
        BossProjectile_lsy bossProjectile = bossProjectilePooled.GetComponent<BossProjectile_lsy>();

        bossProjectile.transform.position = newPosition;
        bossProjectilePooled.SetActive(true);
        bossProjectile.EnableBossProjectile();

        return bossProjectile;
    }

    protected void ShootProjectile(BossProjectile_lsy bossProjectile, float speed, float angle, float acceleration)
    {
        if (bossProjectile == null)
        {
            return;
        }

        bossProjectile.Shoot(angle, speed, acceleration);
    }

    protected virtual void EnableShooting()
    {
        isShooting = true;
    }

    protected virtual void DisableShooting()
    {
        isShooting = false;
    }
}

