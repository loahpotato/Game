using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Item Weapon")]
public class ItemData_lsy : ScriptableObject
{
    public Weapon_lsy WeaponToEquip;
    public Sprite WeaponSprite;
}
