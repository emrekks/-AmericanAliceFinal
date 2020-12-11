using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private WeaponController _weaponController;
    public float speed = 5f;
    [SerializeField] private Rigidbody _rigidbody;
    public GameObject staffPrefab;


    void Start()
    {
        _rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}
