using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponents_lsy : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;

    protected CharacterController_lsy controller;
    protected CharacterMovement_lsy characterMovement;
    protected PlayerWeapon playerWeapon;
    protected CharacterWeapon_lsy characterWeapon;
    protected Animator animator;
    protected Character_lsy character;
    protected EnemyWeapon enemyWeapon;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController_lsy>();
        character = GetComponent<Character_lsy>();
        characterWeapon = GetComponent<CharacterWeapon_lsy>();
        playerWeapon = GetComponent<PlayerWeapon>();
        enemyWeapon = GetComponent<EnemyWeapon>();
        characterMovement = GetComponent<CharacterMovement_lsy>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        HandleAbility();
    }

    // Main method. Here we put the logic of each ability
    protected virtual void HandleAbility()
    {
        InternalInput();
        HandleWeapon();
        HandleInput();
    }


    protected virtual void HandleWeapon()
    {
    }

    // Here we get the necessary input we need to perform our actions    
    protected virtual void HandleInput()
    {

    }

    // Here get the main input we need to control our character
    protected virtual void InternalInput()
    {
        if (character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }
}
