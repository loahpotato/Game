using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLoadScene_lsy : Door_lsy
{
    public GameObject Boss { get; set; }
    //public Character character;

    protected override void Update()
    {
        Key = KeyManager_lsy.Instance.theKey;
        Boss = GameObject.FindWithTag("Boss");
        if ( Boss == null)
        {
            canOpenDoor = true;
        }
        else
        {
            canOpenDoor = false;
        }

        inDoorLoadScene();

    }


}
