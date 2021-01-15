using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public GameObject alice;
    public bool activated;
    private ThrowAxe _throwAxe;
    public float rotationSpeed = -1400;
    public bool isAxeHand;
    public BoxCollider AxeCollider;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _throwAxe = GameObject.FindObjectOfType<ThrowAxe>();
    }
    void Update()
    {
        isAxeHand = _throwAxe.AxeControllerIsHand;
       
        if (isAxeHand && _playerController.AttackingColliderEnable)
        {
            AxeCollider.enabled = true;
        }

        if (isAxeHand && !_playerController.AttackingColliderEnable)
        {
            AxeCollider.enabled = false;
        }
        else
        {
            AxeCollider.enabled = true;
        }

        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        //GetComponent<Rigidbody>().Sleep();
        //GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;
        activated = false;
        _throwAxe.hitedWall = true;
        //_throwAxe.weapon.localEulerAngles = new Vector3(-180, 270, -130);
        _throwAxe.weapon.localEulerAngles = new Vector3(alice.transform.localEulerAngles.x, alice.transform.localEulerAngles.y + 100, 60);
    }

}
