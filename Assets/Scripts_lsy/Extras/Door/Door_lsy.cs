using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using System.Reflection;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Door_lsy : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject NoKeyPopUpPanel;
    [SerializeField] private GameObject BottomPanel;

    [Header("Position")]
    [SerializeField] private Transform DoorToGo;
    protected int Key;
    //[Header("Items")]
    //[SerializeField] private VendorItem weaponItem;
   // [SerializeField] private VendorItem healthItem;
   // [SerializeField] private VendorItem shieldItem;

    protected bool canOpenDoor;
    private Character_lsy character;

    protected virtual void Update()
    {
        Key = KeyManager_lsy.Instance.theKey;
        if (Key > 0)
        {
            canOpenDoor = true;
        }
        else
        {
            canOpenDoor = false;
        }

        inDoor();
    }

    protected virtual void inDoor()
    {
        if (BottomPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            character.transform.localPosition = DoorToGo.position;
        }

    }

    protected virtual void inDoorDropWeapon()
    {
        if (BottomPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            character.transform.localPosition = DoorToGo.position;
            character.GetComponent<PlayerWeapon>().DropWeapon();
        }

    }
    protected virtual void inDoorLoadScene()
    {
        if (BottomPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            character.GetComponent<PlayerWeapon>().DropWeapon();
            SceneManager.LoadScene("yyc");
            canOpenDoor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            character = other.GetComponent<Character_lsy>();
            if (!canOpenDoor)
            {
                BottomPanel.SetActive(false);
                NoKeyPopUpPanel.SetActive(true);
            }
            else
            {
                BottomPanel.SetActive(true);
                NoKeyPopUpPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            character = null;
            //canOpenShop = false;
            NoKeyPopUpPanel.SetActive(false);
            BottomPanel.SetActive(false);
        }
    }

}
