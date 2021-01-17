using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoAttack : MonoBehaviour
{
    public Animator anim;
    public PlayerController _player;
    public RhinoEnemy _rhinoEnemy;

    public bool hitPlayer = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _rhinoEnemy.isCharging == true && hitPlayer == false)
        {
            hitPlayer = true;
            anim.SetTrigger("DownBack");
            Debug.Log("Hit");
            _player.controller.Move(Vector3.forward * Time.deltaTime * -32);
            _player.playerHealth -= 50;
            _rhinoEnemy.isTired = true;
            _rhinoEnemy.isCharging = false;
        }
    }
}
