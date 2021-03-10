using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun_lsy : CharacterComponents_lsy
{
    [SerializeField] private float runSpeed = 12f;

    protected override void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            Run();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            StopRun();

    }

    private void Run()
    {
        characterMovement.speed = runSpeed;
    }

    private void StopRun()
    {
        characterMovement.ResetSpeed();
    }
}
