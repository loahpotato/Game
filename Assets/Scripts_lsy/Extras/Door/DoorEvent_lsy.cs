using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent_lsy : Door_lsy
{
    public GameObject Classmate { get; set; }
    //public Character character;

    protected override void Update()
    {
        Key = KeyManager_lsy.Instance.theKey;
        Classmate = GameObject.FindWithTag("Classmate");
        if (Key > 0 && Classmate == null)
        {
            canOpenDoor = true;
            
            //character.GetComponent<PlayerWeapon>().weapon = null;
        }
        else
        {
            canOpenDoor = false;
        }

        inDoorDropWeapon();

    }


}
