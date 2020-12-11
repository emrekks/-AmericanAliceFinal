using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float shootDelay = 0.1f;
    public GameObject ballPrefab;
    private ProjectileController _projectileController;

    private float delay = 0f;
    public bool isHandlingWand = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
    public void ShootBall()
    {
        if (isHandlingWand == true)
        {
            Instantiate(ballPrefab);
        }
    }
    
    
    
}
