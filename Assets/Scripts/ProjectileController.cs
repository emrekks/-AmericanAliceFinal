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
    public Camera cam;


    public void Start()
    {
        cam = FindObjectOfType<Camera>();
        crossPos = cam.GetComponentInChildren<Transform>();
    }

    void OnBecameVisible()
    {
        _rigidbody.AddForce((-crossPos.transform.position + transform.forward) * speed, ForceMode.Impulse);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
