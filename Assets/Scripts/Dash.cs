using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour


{

    public PlayerController Player;
    public float dashSpeed;
    public float dashTime;
    public ParticleSystem _dash;
    public GameObject _dashobject;

    public SkinnedMeshRenderer SMD;

    public float startTime;

    public GameObject weapon1, weapon2;

    private bool weapon1off;
    private bool weapon2off;

    private bool weapon1changed;
    private bool weapon2changed;
    public AudioSource dashAudio;
    
    
    
    

    

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Tab) && Player.playerDead == false)
        {
            
             if (Player.staff.activeInHierarchy)
             {
                 weapon1changed = true;
                 weapon1.SetActive(false);
                 weapon1off = true;
                 weapon2off = false;
             }
             
                         
             if (Player.axeObject.activeInHierarchy)
             {
                 weapon2changed = true;
                 weapon2.SetActive(false);
                 weapon2off = true;
                 weapon1off = false;
             }

             dashAudio.Play();
             _dashobject.SetActive(true);
             
            StartCoroutine(Dashs());
            
            StartCoroutine(particleDash());
            
        }
        
    }
    
    

    IEnumerator Dashs()
    {
        startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            Player.controller.Move(Player.moveDir * dashSpeed * Time.deltaTime);

            SMD.enabled = false;

            yield return null;
            

        }
        
        
    }
    
    IEnumerator particleDash()
    {

        yield return new WaitForSeconds(1f);
        
        _dashobject.SetActive(false);
        
        
        SMD.enabled = true;
        
        if (weapon1off == true && weapon1changed == true && weapon2off == false)
        {
            weapon1.SetActive(true);
        }
        
        
        if (weapon2off == true && weapon2changed == true && weapon1off == false)
        {
            weapon2.SetActive(true);
        }
        
        

        
        
       

    }
    
}
