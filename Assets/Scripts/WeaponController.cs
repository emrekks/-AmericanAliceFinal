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
        if (isHandlingWand == true)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                delay = shootDelay;
                ShootBall();
            }
        }
    }

    
    public void ShootBall()
    {
        
    }
    
    
}
