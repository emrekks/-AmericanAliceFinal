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

    public CinemachineFreeLook cam;
    public CinemachineFreeLook AimLockCam;
    private float maxFOV = 60;
    private float minFOV = 45;
    public bool isAim = false;

    public Animator anim;

    public MeshRenderer aimLockSprite1;
    public MeshRenderer aimLockSprite2;
    public MeshRenderer aimLockSprite3;

    public AutoAim _Autoaim;
    public PlayerController player;
    public GameObject aimLockAnim;
    public GameObject Alice;
    public GameObject crossHair;

    public bool autoAimTrue = false;

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
        if(player.isHandlingWand && _Autoaim.closestEnemy != null)
        {
            aimLockSprite1.enabled = true;
            aimLockSprite2.enabled = true;
            aimLockSprite3.enabled = true;
            crossHair.SetActive(false);
            autoAimTrue = true;
            aimLockAnim.transform.position = new Vector3(_Autoaim.closestEnemy.transform.position.x, _Autoaim.closestEnemy.transform.position.y + 1.5f, _Autoaim.closestEnemy.transform.position.z);
        }
        else
        {
            aimLockSprite1.enabled = false;
            aimLockSprite2.enabled = false;
            aimLockSprite3.enabled = false;
            crossHair.SetActive(true);
            autoAimTrue = false;
        }



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
