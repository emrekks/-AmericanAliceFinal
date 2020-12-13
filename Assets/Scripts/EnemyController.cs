using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float enemyDistanceRun;


    #region EnemyStats

    public float enemyHealth;
    public float enemyArmor;
    public float enemyDamage;

    #endregion

    public float enemyBlockTimer = 0f;


    public bool playerSeen = false;

    public Animator Anim;

    public float enemyAttackTimer = 0f;
    
    
    //Enemy Move Radius
    private float moveTimer = 0f;
    private float moveX = -10;
    private float moveY = 10;

    //Enemy Trigger Radius
    public float lookRadius = 10f;
    
    //Enemy Attack Radius
    public float attackRadius = 1f;

    private Transform _target;
    public NavMeshAgent _agent;

    void Start()
    {
        _target = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);
        
        //float attackDistance = Vector3.Distance(_target.position, transform.position);

        //Enemy Chase
        if (player.GetComponent<PlayerController>().devilOpen == false)
        {
            if (distance <= lookRadius)
            {
                playerSeen = true;
                _agent.SetDestination(_target.position);

                if (distance <= _agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
            else
            {
                playerSeen = false;
            }
            
            
            if (distance <= attackRadius)
            {
                Debug.Log("Attacked");
                Anim.SetTrigger("Attack");
                
            }
        }
        
        //Enemy Run Away
        else
        {
            if (distance < enemyDistanceRun)
            {
                Vector3 toPlayer = transform.position - player.transform.position;

                Vector3 newPos = transform.position + toPlayer;
                _agent.SetDestination(newPos);
            }

            //If Player Seen
            if (playerSeen == true)
            {
                EnemyBlock();
            }
            
        }
    }

    //Enemy turns towards us
    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    //Enemy Block
    void EnemyBlock()
    {
        if (enemyHealth < 50)
        {
            enemyBlockTimer += Time.deltaTime;

            if (enemyBlockTimer <= 3f)
            {
                //Block Animation
                enemyArmor = enemyArmor + 5;
            }
        }
    }




    //Enemy Distance Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
