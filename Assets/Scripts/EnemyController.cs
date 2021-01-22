using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    void Awake()
    {
        Instance = this;
    }


    public GameObject player;
    public float enemyDistanceRun;


    #region EnemyStats

    public float enemyHealth;
    public float enemyArmor;
    public float enemyDamage;

    #endregion

   //public float enemyBlockTimer = 0f;


   

    public bool playerSeen = false;

    public Animator Anim;

    public bool isDead = false;

    public float enemyAttackTimer = 0f;
    private bool isAttack = false;
    private bool isBlock = false;

    public Collider axeCol;
    public Collider ballCol;

    private EnemyController enemyController;

    
    //Enemy Block Radius
    private bool isBlocking = false;
    private bool playerCanAttack = false;
    private float playerCanAttackRadius = 6f;

    //Enemy Move Radius
    private float moveTimer = 0f;
    private float moveX = -10;
    private float moveY = 10;

    //Enemy Trigger Radius
    public float lookRadius = 50f;
    public float chaseRadius = 15f;
    
    //Enemy Attack Radius
    public float attackRadius = 1f;

    private Transform _target;
    public NavMeshAgent _agent;

    private Collider thisEnemyCollider;

    public GameObject _enemy;

    void Start()
    {
        _target = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        thisEnemyCollider = gameObject.GetComponent<Collider>();
        _enemy = gameObject;
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);
        
        //float attackDistance = Vector3.Distance(_target.position, transform.position);

        if (distance <= chaseRadius)
        {
            FaceTarget();
        }
        
        
        

        //Enemy Chase
        if (player.GetComponent<PlayerController>().devilOpen == false)
        {
            if (distance <= lookRadius)
            {
                Anim.SetFloat("speed",_agent.speed);
                playerSeen = true;
                FaceTarget();
                lookRadius = 30f;
                _agent.SetDestination(_target.position);

                if (distance <= _agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
            else
            {
                playerSeen = false;
                lookRadius = 10f;
            }
            
            
            if (distance <= attackRadius || this.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttack = true;
                _agent.speed = 0;               
            }
            else
            {
                _agent.speed = 5;
                isAttack = false;
            }
            
            Anim.SetBool("attack",isAttack);
            //Anim.SetBool("block",isBlock);
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
                
            }
        }
        
        //Enemy Death
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    //Enemy turns towards us
    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    
    
    /*//Enemy Block
    void EnemyBlock()
    {
        if (enemyHealth < 100)
        {
            isBlock = true;
            enemyBlockTimer += Time.deltaTime;

            if (enemyBlockTimer <= 3f)
            {
                
                
                enemyArmor = enemyArmor + 5;
            }
            else
            {
                isBlock = false;
            }
        }
    }*/

    void EnemyHit()
    {
        Anim.SetTrigger("hit");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Axe" || other.gameObject.tag == "MagicBall")
        {
            enemyHealth -= /*playerDamage*/ 50;
            EnemyHit();
            UnityEngine.Debug.Log("Collider hit");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Axe")
        {
            enemyHealth -= /*playerDamage*/ 50;
            EnemyHit();
        }

    }



    void EnemyDeath()
    {
        isDead = true;
        thisEnemyCollider.enabled = false;
        Anim.SetBool("death",isDead);
        // Anim.SetTrigger("death");
        enemyController.enabled = false;
        _agent.enabled = false;
        //thisEnemyCollider.enabled = false;
    }




    //Enemy Distance Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,chaseRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
