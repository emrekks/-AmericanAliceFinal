using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    #endregion


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
