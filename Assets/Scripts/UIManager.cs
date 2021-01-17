using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");

    }

}
