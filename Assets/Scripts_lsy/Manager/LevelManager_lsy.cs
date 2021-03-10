using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager_lsy : Singleton_lsy<LevelManager_lsy>
{
    [SerializeField] private Character_lsy playableCharacter;
    [SerializeField] private Transform spawnPosition;

    public Transform Boss { get; set; }
    public Transform Player { get; set; }

    private void Awake()
    {
        Boss = GameObject.Find("Enemy Boss/Boss").transform;
        Player = playableCharacter.transform;
        Camera2D_lsy.Instance.Target = playableCharacter.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReviveCharacter();
        }
    }

    private void ReviveCharacter()
    {
        if (playableCharacter.GetComponent<Health_lsy>().CurrentHealth <= 0)
        {
            playableCharacter.GetComponent<Health_lsy>().Revive();
            SceneManager.LoadSceneAsync("items", LoadSceneMode.Additive);
            playableCharacter.transform.position = spawnPosition.position;
            playableCharacter.GetComponent<PlayerWeapon>().DropWeapon();
        }
    }
}
