using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon_lsy : Collectables_lsy
{
    [SerializeField] private ItemData_lsy itemWeaponData;

    protected override void Pick()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (character != null)
        {
            character.GetComponent<PlayerWeapon>().weapon = itemWeaponData.WeaponToEquip;
            //character.GetComponent<PlayerWeapon>().isWeapon = false;

        }
    }
}
