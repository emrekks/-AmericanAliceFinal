using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public bool activated;
    private ThrowAxe _throwAxe;
    public float rotationSpeed = -1400;

    private void Start()
    {
        _throwAxe = GameObject.FindObjectOfType<ThrowAxe>();
    }
    void Update()
    {

        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;

        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().Sleep();
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;
        activated = false;
        _throwAxe.hitedWall = true;
        _throwAxe.weapon.localEulerAngles = new Vector3(-180, 270, -130);
    }

}
