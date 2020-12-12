using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private WeaponController _weaponController;
    public float speed = 50f;
    [SerializeField] private Rigidbody _rigidbody;
    public GameObject staffPrefab;
    public GameObject crosshair;
    private Transform crossPos;
    //public GameObject cam;

    private Transform _target;
    public float attackRange = 10f;
    private GameObject player;


    public void Start()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        player = GameObject.FindGameObjectWithTag("Player");
        _target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance > attackRange)
        {
            gameObject.SetActive(false);
        }

    }

    void OnBecameVisible()
    {
        _rigidbody.AddForceAtPosition(transform.forward * speed,crosshair.transform.position, ForceMode.Impulse);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.transform.position,attackRange);
    }
    
}
