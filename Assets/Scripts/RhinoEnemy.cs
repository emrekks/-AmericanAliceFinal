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


    public GameObject rhino;
    public PlayerController _player;
    public float lookRadius = 15f;
    public Animator anim;
    public RhinoAttack _rhinoAttack;

    private Transform _target;
    public NavMeshAgent _agent;

    [SerializeField]private bool playerSeen = false;

    private float stoppingDistance = 1f;
    [SerializeField]private bool inStoppingDistance = false;

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
    public bool isCharging = false;
    private float tiredTimer = 0f;
    public bool isTired = false;
    private bool isStopped;


    #endregion
    
    void Start()
    {
        _target = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
        stoppingDistance = _agent.stoppingDistance;
        isStopped = gameObject.GetComponent<NavMeshAgent>().isStopped;
        rhino = this.gameObject;
        _agent.speed = 2f;
    }

    
    void Update()
    { 
        float distance = Vector3.Distance(_target.position, transform.position);


        if (distance <= _agent.stoppingDistance)
        {
            inStoppingDistance = true;
        }
        else
        {
            inStoppingDistance = false;
        }

        if (_rhinoAttack.hitPlayer == true)
        {
            isStopped = true;
            _agent.Stop();
        }
        else
        {
            isStopped = false;
            _agent.Resume();
        }
        
        
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
        if (distance <= chargeAttackRadius && isSmashing == false)
        {
            chargeTimer += Time.deltaTime;
            
            if (isCharging == false && isTired == false)
            {
                _agent.speed = 0f;
                //ChargeLoadAnim
                Debug.Log("isCharging");
                isCharging = true;                
            }
            
            if (chargeTimer >= 2f)
            {
                chargeStartedTimer += Time.deltaTime;

                if (isCharging == true && isTired == false)  // Bu kısım sadece animasyon bitene kadar çalışıp geçmeli
                {
                    _agent.speed = 8f;
                    //ChargeAnim
                    Debug.Log("Charged");
                }
                
                if (chargeStartedTimer >= 2f || _rhinoAttack.hitPlayer == true)
                {
                    _agent.speed = 0f;
                    tiredTimer += Time.deltaTime;
                    isTired = true;
                    isCharging = false;

                }
                
                if (tiredTimer >= 2f && isTired == true)
                {
                    Debug.Log("Tired");
                    chargeTimer = 0f;
                    chargeStartedTimer = 0f;
                    tiredTimer = 0f;
                    isTired = false;
                    _rhinoAttack.hitPlayer = false;

                }
            }
        }
        else
        {
            isCharging = false;
        }

        //RhinoSmash
        if (distance <= smashAttackRadius)
        {
            smashHitTimer += Time.deltaTime;

            if (isCharging == false && isTired == false && isSmashing == false)
            {
                //SmashLoadAnim
                Debug.Log("isSmashing");
                isSmashing = true;
                _agent.speed = 0f;
            }
            
            if (smashHitTimer >= 1f && isSmashing == true)
            {
                //SmashAnim
                Debug.Log("Smashed");
                smashHitTimer = 0f;
                isSmashing = false;
                _agent.speed = 2f;
            }
        }
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     if (/*other.gameObject.tag == "Player"*/ inStoppingDistance == true)
    //     {
    //         
    //         //_player._rigidbody.AddExplosionForce(1f, rhino.transform.position * -1, 3f, 1f, ForceMode.Impulse);
    //         
    //
    //     }
    // }

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
