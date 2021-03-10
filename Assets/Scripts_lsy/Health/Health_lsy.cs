using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_lsy : MonoBehaviour
{
    public static Action OnBossDead;

    [Header("Health")]
    [SerializeField] private float initialHealth = 3f;
    [SerializeField] private float maxHealth = 3f;

    [Header("Shield")]
    [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;

    [Header("Settings")]
    [SerializeField] private bool destroyObject;
    [SerializeField] private int recoilForce = 60;

    private Character_lsy character;
    private CharacterController_lsy controller;
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth_lsy enemyHealth;
    private BossBaseShot_lsy bossBaseShot;

    private bool isPlayer;
    private bool shieldBroken;

    // Controls the current health of the object    
    public float CurrentHealth { get; set; }

    // Returns the current health of this character
    public float CurrentShield { get; set; }

    private void Awake()
    {
        character = GetComponent<Character_lsy>();
        controller = GetComponent<CharacterController_lsy>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth_lsy>();
        bossBaseShot = GetComponent<BossBaseShot_lsy>();

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        if (character != null)
        {
            isPlayer = character.CharacterType == Character_lsy.CharacterTypes.Player;
        }

        UpdateCharacterHealth();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
        if (CurrentShield > 0)
        {
            shieldBroken = false;
        }

    }

    // Take the amount of damage we pass in parameters
    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        if (!shieldBroken && character != null && initialShield > 0)
        {
            CurrentShield -= damage;

            UpdateCharacterHealth();

            if (CurrentShield <= 0)
            {
                shieldBroken = true;
            }
            return;
        }

        DamageRecoil();
        CurrentHealth -= damage;

        UpdateCharacterHealth();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void DamageRecoil()
    {
        if (character != null)
        {

            if (character.GetComponent<CharacterFlip_lsy>().FacingRight)
            {
                Vector2 newPosition = new Vector2(character.transform.position.x - recoilForce, character.transform.position.y);
                controller.SetMovement(newPosition);
                controller.SetJump(recoilForce/20);
            }
        
            else
            {
                Vector2 newPosition = new Vector2(character.transform.position.x + recoilForce, character.transform.position.y);
                controller.SetMovement(newPosition);
                controller.SetJump(recoilForce / 20);
            }
        }
        
    }

    public void StopRecoil()
    {
        controller.ApplyRecoil(Vector2.one, 0f);
    }

    // Kills the game object
    private void Die()
    {
        if (character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;
        }

        if (bossBaseShot != null)
        {
            OnBossDead?.Invoke();
        }

        if (destroyObject)
        {
            DestroyObject();
        }
    }

    // Revive this game object    
    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
            controller.enabled = true;
        }

        gameObject.SetActive(true);

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        shieldBroken = false;

        UpdateCharacterHealth();
    }

    public void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        UpdateCharacterHealth();
    }

    public void GainShield(int amount)
    {
        CurrentShield = Mathf.Min(CurrentShield + amount, maxShield);
        UpdateCharacterHealth();
    }

    // If destroyObject is selected, we destroy this game object
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    private void UpdateCharacterHealth()
    {
        // Update Enemy health
        if (enemyHealth != null && bossBaseShot == null)
        {
            enemyHealth.UpdateEnemyHealth(CurrentHealth, maxHealth);
        }

        // Update Boss health
        if (bossBaseShot != null)
        {
            UIManager_lsy.Instance.UpdateBossHealth(CurrentHealth, maxHealth);
        }

        // Update Player health
        if (character != null && bossBaseShot == null)
        {
            UIManager_lsy.Instance.UpdateHealth(CurrentHealth, maxHealth,  isPlayer);
        }
    }
}
