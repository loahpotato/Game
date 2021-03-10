using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager_lsy : Singleton_lsy<KeyManager_lsy>
{
    public int theKey { get; set; }

    private readonly string KEY = "Classroom key";

    private void Start()
    {
        PlayerPrefs.SetInt(KEY, 0);
        theKey = 0;
        //checkKey();
    }

    public void AddKey(int isKey)
    {
        
        PlayerPrefs.SetInt(KEY, isKey);
        theKey = isKey;
    }

}
