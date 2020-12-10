using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private PlayerController _playerController;
    
    //Enemy Trigger Radius
    public float lookRadius = 10f;

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

        if (distance <= lookRadius)
        {
            _agent.SetDestination(_target.position);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
