using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float shootDelay = 0.1f;
    public GameObject ballPrefab;
    private ProjectileController _projectileController;
    private GameObject[] magicBallPool;
    public int size;
    public GameObject magicBallSpawnPoint;

    private float delay = 0f;
    public bool isHandlingWand = false;

    public CinemachinePath path;
    
    

    void Start()
    {

        magicBallPool = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            magicBallPool[i] = Instantiate(ballPrefab);
            magicBallPool[i].SetActive(false);
        }

    }

    void Update()
    {
        //Attack
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            delay = shootDelay;
            ShootBall();
        }

        if (Input.GetMouseButton(1))
        {
            
        }
        
        
        
    }

    
    public void ShootBall()
    {
        for (int i = 0; i < size; i++)
        {
            if (!magicBallPool[i].activeInHierarchy)
            {
                magicBallPool[i].transform.position = magicBallSpawnPoint.transform.position + transform.forward;
                magicBallPool[i].transform.rotation = magicBallSpawnPoint.transform.rotation;
                magicBallPool[i].SetActive(true);
                return;
            }
        }
    }
    
    
    
}
