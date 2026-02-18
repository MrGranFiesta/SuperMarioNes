using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager 
{
    private GameObject _go;

    public FireManager()
    {
        _go = new GameObject("FireManager");
        Object.DontDestroyOnLoad(_go);
        _go.AddComponent<FirePooling>();
    }
}
