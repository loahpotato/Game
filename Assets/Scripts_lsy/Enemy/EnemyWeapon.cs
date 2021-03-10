using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : CharacterComponents_lsy
{
    public static Action OnStartShooting;

    [Header("Weapon Settings")]
    [SerializeField] private Weapon_lsy weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    public bool isWeapon = false;
    public Weapon_lsy weapon { get; set; }

    // Reference of the Weapon we are using
    public Weapon_lsy CurrentWeapon { get; set; }

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

            if (Input.GetMouseButtonUp(0))
            {
                StopWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
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
            //WeaponAim.DestroyReticle();       // Each weapon has its own Reticle component
            Destroy(GameObject.Find("Pool"));
            Destroy(CurrentWeapon.gameObject);
        }

        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);
        //WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

        if (character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            UIManager_lsy.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
            UIManager_lsy.Instance.UpdateWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }
}
