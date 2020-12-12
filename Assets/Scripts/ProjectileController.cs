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
    public Vector3 crosshairTransform;
    public float ballExplosionRadius = 10f;
    public float upwardsModifier = 5f;


    void OnBecameVisible()
    {
        _rigidbody.AddForceAtPosition(transform.up + transform.forward * speed * 5, crosshairTransform, ForceMode.Impulse);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
