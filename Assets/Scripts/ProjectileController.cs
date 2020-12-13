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
    private GameObject crosshair;
    private Transform crossPos;
    // public GameObject magicBallSpawnPoint;

    private Transform _target;
    public float attackRange = 10f;
    private GameObject player;
    private float ballTimer = 0f;


    void Awake()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Start()
    {
        _target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        ballTimer += Time.deltaTime;
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance > attackRange || ballTimer >= 5f)
        {
            gameObject.SetActive(false);
            ballTimer = 0f;
            _rigidbody.isKinematic = true;
        }
    }

    void OnEnable()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForceAtPosition(crosshair.transform.forward * speed,crosshair.transform.position, ForceMode.Impulse);
    }
    
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.transform.position,attackRange);
    }
    
}
