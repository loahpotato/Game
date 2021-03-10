using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_lsy : Singleton_lsy<UIManager_lsy>
{
    [Header("Settings")]
    [SerializeField] private Image healthBar;
    //[SerializeField] private Image shieldBar;
    [SerializeField] private TextMeshProUGUI currentHealthTMP;

    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI currentAmmoTMP;
    [SerializeField] private Image weaponImage;

    [Header("Boss")]
    [SerializeField] private Image bossHealth;
    [SerializeField] private GameObject bossHealthBarPanel;
    [SerializeField] private GameObject bossIntroPanel;

    private float playerCurrentHealth;
    private float playerMaxHealth;
    private bool isPlayer;
    private int playerCurrentAmmo;
    private int playerMaxAmmo;

    private float bossCurrentHealth;
    private float bossMaxHealth;

    private void Update()
    {
        InternalUpdate();
    }

    public void UpdateHealth(float currentHealth, float maxHealth, bool isThisMyPlayer)
    {
        playerCurrentHealth = currentHealth;
        playerMaxHealth = maxHealth;
        isPlayer = isThisMyPlayer;
    }
    public void UpdateBossHealth(float currentHealth, float maxHealth)
    {
        bossCurrentHealth = currentHealth;
        bossMaxHealth = maxHealth;
    }


    public void UpdateWeaponSprite(Sprite weaponSprite)
    {
        weaponImage.sprite = weaponSprite;
        weaponImage.SetNativeSize();
    }


    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        playerCurrentAmmo = currentAmmo;
        playerMaxAmmo = maxAmmo;
    }

    private void InternalUpdate()
    {
        if (isPlayer)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerCurrentHealth / playerMaxHealth, 10f * Time.deltaTime);
            currentHealthTMP.text = playerCurrentHealth.ToString() + "/" + playerMaxHealth.ToString();
        }

        // Update Boss Health
        bossHealth.fillAmount = Mathf.Lerp(bossHealth.fillAmount, bossCurrentHealth / bossMaxHealth, 10f * Time.deltaTime);

        // Update Ammo
        currentAmmoTMP.text = playerCurrentAmmo + " / " + playerMaxAmmo;

    }
    private IEnumerator BossFight()
    {
        bossIntroPanel.SetActive(true);
        StartCoroutine(MyLibrary_lsy.FadeCanvasGroup(bossIntroPanel.GetComponent<CanvasGroup>(), 1f, 1f));

        // Move Camera -> Boss
        Camera2D_lsy.Instance.Target = LevelManager_lsy.Instance.Boss;
        Camera2D_lsy.Instance.Offset = new Vector2(0f, -3f);  // Depends on personal setting on Boss location

        yield return new WaitForSeconds(3f);

        // Go back to the player
        Camera2D_lsy.Instance.Target = LevelManager_lsy.Instance.Player;
        Camera2D_lsy.Instance.Offset = Camera2D_lsy.Instance.PlayerOffset;

        // Show Boss HealthBar
        StartCoroutine(MyLibrary_lsy.FadeCanvasGroup(bossIntroPanel.GetComponent<CanvasGroup>(), 1f, 0f, () =>
        {
            bossIntroPanel.SetActive(false);
            bossHealthBarPanel.SetActive(true);
            StartCoroutine(MyLibrary_lsy.FadeCanvasGroup(bossHealthBarPanel.GetComponent<CanvasGroup>(), 1f, 1f));
        }));
    }

    private void OnBossDead()
    {
        StartCoroutine(MyLibrary_lsy.FadeCanvasGroup(bossHealthBarPanel.GetComponent<CanvasGroup>(), 1f, 0f, () =>
        {
            bossHealthBarPanel.SetActive(false);
        }));
    }

    private void OnEventResponse(GameEvent_lsy.EventType obj)
    {
        switch (obj)
        {
            case GameEvent_lsy.EventType.BossFight:
                StartCoroutine(BossFight());
                break;
        }
    }

    private void OnEnable()
    {
        GameEvent_lsy.OnEventFired += OnEventResponse;
        Health_lsy.OnBossDead += OnBossDead;
    }

    private void OnDisable()
    {
        GameEvent_lsy.OnEventFired -= OnEventResponse;
        Health_lsy.OnBossDead -= OnBossDead;
    }

}
