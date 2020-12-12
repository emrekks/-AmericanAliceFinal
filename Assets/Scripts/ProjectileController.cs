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
    public Transform crosshair;


    void OnBecameVisible()
    {
        _rigidbody.AddForce( crosshair.transform.position * speed, ForceMode.Impulse);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
