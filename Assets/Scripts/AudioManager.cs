using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    #region Singleton

    public static AudioManager instance;
    
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
