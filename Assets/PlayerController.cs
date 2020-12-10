using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerGravity;
    public Transform Groundcheck;
    public float groundRadius = 2.0f;
    public LayerMask whatIsGround;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 6f;
    private float gravityValue = -9.0f;
    private float turnSmoothtime = 0.03f;
    private float turnSmoothVelocity;
    public Transform Cam;
    private bool grounded;

    private void Start()
    {

    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        grounded = Physics.CheckSphere(Groundcheck.position, groundRadius, whatIsGround);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }

        //grounded = Physics.CheckSphere(Groundcheck.position, groundRadius, whatIsGround);
        Debug.Log(grounded);
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Debug.Log("jump");
            playerGravity.y = jumpHeight;
        }
        else
        {
            playerGravity.y += gravityValue * Time.deltaTime;
        }
        controller.Move(playerGravity * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
