using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool_lsy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPS;

    private Projectile_lsy projectile;
    private BossProjectile_lsy bossProjectile;

    private void Start()
    {
        projectile = GetComponent<Projectile_lsy>();
        bossProjectile = GetComponent<BossProjectile_lsy>();
    }

    // Returns this object to the pool
    private void Return()
    {
        if (projectile != null)
        {
            projectile.ResetProjectile();
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MyLibrary_lsy.CheckLayer(other.gameObject.layer, objectMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();
            }

            if (bossProjectile != null)
            {
                bossProjectile.DisableBossProjectile();
            }

            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
    }

    /*  REMOVE this method because we will put this into the MyLibrary class
    private bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }
    */

    private void OnEnable()
    {
        if (lifeTime > 0)
        {
            Invoke(nameof(Return), lifeTime);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
