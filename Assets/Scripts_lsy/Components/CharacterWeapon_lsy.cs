using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon_lsy : CharacterComponents_lsy
{
    public static Action OnStartShooting;

    [Header("Weapon Settings")]
    [SerializeField] private Weapon_lsy weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    // Reference of the Weapon we are using
    public Weapon_lsy CurrentWeapon { get; set; }

    // Store the reference to the second weapon
    public Weapon_lsy SecondaryWeapon { get; set; }

    public Weapon_lsy TertiaryWeapon { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }

    protected override void HandleInput()
    {
        if (character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }

            if (Input.GetMouseButtonUp(0))  // If we stop shooting
            {
                StopWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && (SecondaryWeapon != null || TertiaryWeapon != null))
            {
                EquipWeapon(weaponToUse, weaponHolderPosition);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
            {
                EquipWeapon(SecondaryWeapon, weaponHolderPosition);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && TertiaryWeapon != null)
            {
                EquipWeapon(TertiaryWeapon, weaponHolderPosition);
            }

        }
    }

    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.UseWeapon();
        if (character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            OnStartShooting?.Invoke();
            UIManager_lsy.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    // When we stop shooting we stop using our Weapon
    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.StopWeapon();
    }

    public void Reload()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Reload();
        if (character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            UIManager_lsy.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }

    }

    public void EquipWeapon(Weapon_lsy weapon, Transform weaponPosition)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.WeaponAmmo.SaveAmmo();
           
            Destroy(GameObject.Find("Pool"));
            Destroy(CurrentWeapon.gameObject);
        }

        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);

        if (character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            UIManager_lsy.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
            UIManager_lsy.Instance.UpdateWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }
}
