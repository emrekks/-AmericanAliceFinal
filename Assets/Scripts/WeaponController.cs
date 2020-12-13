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

    public CinemachineFreeLook cam;
    private float maxFOV = 60;
    private float minFOV = 45;
    public bool isAim = false;

    public Animator anim;



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
        anim.SetBool("isAim", isAim);
        
        ////Attack
        //if (delay > 0)
        //{
        //    delay -= Time.deltaTime;
        //}
        //else if (Input.GetMouseButtonDown(0))
        //{
        //    delay = shootDelay;
        //    ShootBall();
        //}

        if (Input.GetMouseButton(1))
        {
            isAim = true;
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, minFOV, 0.1f);


            // cam.m_Lens.FieldOfView = 45f;

            // for (int i = 60; i > 45; i--)
            // {
            //     camFOVTimer -= Time.deltaTime;
            // }
        }
        else
        {
            isAim = false;
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, maxFOV, 0.1f);
            
            
            // cam.m_Lens.FieldOfView = 60f;
            
            // for (int i = 45; i > 60; i++)
            // {
            //     camFOVTimer += Time.deltaTime;
            // }
        }
        
        
        
    }

    
    public void ShootBall()
    {
        for (int i = 0; i < size; i++)
        {
            if (!magicBallPool[i].activeInHierarchy)
            {
                magicBallPool[i].transform.position = magicBallSpawnPoint.transform.position + transform.forward;
                // magicBallPool[i].transform.rotation = magicBallSpawnPoint.transform.rotation;
                magicBallPool[i].SetActive(true);
                return;
            }
        }
    }
    
    
    
}
