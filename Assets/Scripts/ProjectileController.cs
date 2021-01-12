using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private WeaponController weaponController;
    public float speed = 50f;
    [SerializeField] private Rigidbody _rigidbody;
    public GameObject staffPrefab;
    private GameObject crosshair;
    private GameObject aimLock;
    // public GameObject magicBallSpawnPoint;
    private Vector3 up;

    private Transform _target;
    public float attackRange = 10f;
    private GameObject player;
    private float ballTimer = 0f;


    void Awake()
    {
        gameObject.SetActive(false);
        weaponController = GameObject.FindObjectOfType<WeaponController>();
        aimLock = GameObject.FindGameObjectWithTag("AimLockedTarget");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Start()
    {
        _target = PlayerManager.instance.player.transform;
        up = new Vector3(0, 0.1f, 0);
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
        shootBall();
    }

    void shootBall()
    {
        if(weaponController.autoAimTrue == false)
        {
            _rigidbody.AddForceAtPosition((crosshair.transform.forward + up) * speed, crosshair.transform.position, ForceMode.Impulse);
        }
        else if(weaponController.autoAimTrue == true)
        {
            _rigidbody.AddForce(aimLock.transform.position - transform.position, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.transform.position, attackRange);
    }

}
