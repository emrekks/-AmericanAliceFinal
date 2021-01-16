using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using Debug = UnityEngine.Debug;
using Random = System.Random;

public class RhinoEnemy : MonoBehaviour
{
 
    #region EnemyStats

    public float rhinoHealth;
    public float rhinoArmor;
    public float rhinoDamage;

    #endregion


    public float lookRadius = 15f;

    private Transform _target;
    private NavMeshAgent _agent;

    [SerializeField]private bool playerSeen = false;

    #region RhinoSmash
    
    public float smashAttackRadius = 3f;
    public float smashHitRadius = 5f;
    private float smashHitTimer = 0f;
    [SerializeField]private bool isSmashing = false;

    #endregion


    #region RhinoCharge

    public float chargeAttackRadius = 13f;
    private float chargeTimer = 0f;
    private float chargeStartedTimer = 0f;
    [SerializeField]private bool isCharging = false;
    

    #endregion
    
    void Start()
    {
        _target = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        //RhinoLook
        if (distance <= lookRadius)
        {
            playerSeen = true;
            _agent.SetDestination(_target.position);
        }
        else
        {
            playerSeen = false;
        }

        if (playerSeen == true)
        {
            lookRadius = 30f;
            FaceTarget();
        }
        else
        {
            lookRadius = 15f;
            _agent.speed = 2f;
            isCharging = false;
            isSmashing = false;
        }
        
        //RhinoCharge
        if (distance <= chargeAttackRadius)
        {
            chargeTimer += Time.deltaTime;
            
            if (isCharging == false)
            {
                isCharging = true;
                _agent.speed = 0f;
                //ChargeLoadAnim
                Debug.Log("isCharging");
            }
            
            if (chargeTimer >= 2f)
            {
                chargeStartedTimer += Time.deltaTime;

                if (isCharging == true)
                {
                    _agent.speed = 8f;
                    //ChargeAnim
                    Debug.Log("Charged");
                    chargeTimer = 0f;
                }
                
                if (chargeStartedTimer >= 3f)
                {
                    _agent.speed = 2f;
                    isCharging = false;
                    chargeStartedTimer = 0f;
                }
            }
        }

        //RhinoSmash
        if (distance <= smashAttackRadius)
        {
            smashHitTimer += Time.deltaTime;
            //SmashLoadAnim
            Debug.Log("isSmashing");
            isSmashing = true;
            _agent.speed = 0f;

            if (smashHitTimer >= 3f)
            {
                //SmashAnim
                Debug.Log("Smashed");
                smashHitTimer = 0f;
                isSmashing = false;
                _agent.speed = 2f;
            }
        }
        
        
        


    }
    
    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,smashAttackRadius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position,smashHitRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,chargeAttackRadius);
    }
}
