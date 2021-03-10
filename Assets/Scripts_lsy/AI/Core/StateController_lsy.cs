using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.Universal;

public class StateController_lsy : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private AIState_lsy currentState;
    [SerializeField] private AIState_lsy remainState;

    // Returns the target of this Enemy
    public Transform Target { get; set; }

    // Returns a reference to this enemy movement
    public CharacterMovement_lsy CharacterMovement { get; set; }

    // Returns this character weapon
    public CharacterWeapon_lsy CharacterWeapon { get; set; }

    public CharacterFlip_lsy CharacterFlip { get; set; }

    // Returns a reference to this enemy path
    public Path_lsy Path { get; set; }
    public Transform Player { get; set; }
    public Health_lsy PlayerHealth { get; set; }

    // Returns the collider of this enemy
    public Collider2D Collider2D { get; set; }

    public BossCirclePattern_lsy BossCirclePattern { get; set; }
    public BossRandomPattern_lsy BossRandomPattern { get; set; }
    public BossSpiralPattern_lsy BossSpiralPattern { get; set; }

    private void Awake()
    {
        CharacterMovement = GetComponent<CharacterMovement_lsy>();
        CharacterFlip = GetComponent<CharacterFlip_lsy>();
        CharacterWeapon = GetComponent<CharacterWeapon_lsy>();
        Path = GetComponent<Path_lsy>();
        Collider2D = GetComponent<Collider2D>();

        Player = GameObject.FindWithTag("Player").transform;
        PlayerHealth = Player.GetComponent<Health_lsy>();

        BossCirclePattern = GetComponent<BossCirclePattern_lsy>();
        BossRandomPattern = GetComponent<BossRandomPattern_lsy>();
        BossSpiralPattern = GetComponent<BossSpiralPattern_lsy>();

    }

    private void Update()
    {
        currentState.EvaluateState(this);
    }

    public void TransitionToState(AIState_lsy nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
